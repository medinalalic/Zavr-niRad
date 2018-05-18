using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulOsoblje.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Controllers
{
    public class ListaRacunaController : Controller
     {    Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
   
        // GET: ModulOsoblje/ListaRacuna
        public ActionResult Index(int? page, string search, string search2, int? pacijentid)
        {
            
            string s, s1;
            int o = pacijentid ?? 0;

            if (search == null) s = "";
            else s = search;
            if (search2 == null) s1 = "";
            else s1 = search2;
            IPagedList<Pacijent> op;
           
            if (s == "" && s1 == "" && o == 0) op = ctx.Pacijents.Where(c=>c.Korisnik.Aktivan==true).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else if (s != "" || s1 != "" && o == 0) op = ctx.Pacijents.Where(c => c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower()) && c.Korisnik.Aktivan == true).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else if (s == "" || s1 == "" && o != 0) op = ctx.Pacijents.Where(c => c.Id == o && c.Korisnik.Aktivan == true).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else op = ctx.Pacijents.Where(c => c.Id == o && c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower()) && c.Korisnik.Aktivan == true).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
          
            PacijentPagingVM model = new PacijentPagingVM { };
            model.PacijentList = op;


            return View(model);

        }
        private bool IsUneseno(int id)
        {
            Racun c = ctx.Racuns.Where(p => p.PregledId == id).FirstOrDefault();
            if (c != null)
                return true;
            return false;
        }
        public ActionResult Pregledi(int PacijentId)
        {
            List<HistorijaVM.HistorijaInfo> pregledInfo = new List<HistorijaVM.HistorijaInfo>();
            pregledInfo = ctx.Pregleds.ToList().Where(c => c.PacijentId == PacijentId)
                .Select(x => new HistorijaVM.HistorijaInfo
                {
                    Id = x.Id,
                    DatumPregleda = x.DatumPregleda,
                    VrijemePregleda = x.VrijemePregleda,
                    PacijentId= x.PacijentId,
                    Uneseno=IsUneseno(x.Id),
                    


                }).ToList();

            HistorijaVM model = new HistorijaVM
            {
                history = pregledInfo,
             

            };
          
           // model.Uneseno = IsUneseno(item.Id);
            return View(model);
        }
        public ActionResult PregledajRacune(int pregledID,int pacijentID)
        {
            
               
                List<RacunPrikaz.RacunInfo> racunInfo;
                
                    racunInfo = ctx.Racuns.ToList().Where(c=>c.PregledId==pregledID && c.PacijentId==pacijentID)
                        .Select(x => new RacunPrikaz.RacunInfo
                        {
                            Id = x.Id,
                            Uplaćeno = x.Uplaćeno,
                            Cijena = x.Cijena,
                            Datum = x.Datum
                        }).ToList();

            RacunPrikaz model = new RacunPrikaz
            {
                racun = racunInfo,


            };
            return View(model);

        }



        public ActionResult UnosRacuna(int pregledID,int pacijentID)
        {
            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Osoblje p = ctx.Osobljes.Where(x => x.Id == lp.Id).FirstOrDefault();

            RacunIzmijeni Model = new RacunIzmijeni();

            Model.Datum = DateTime.Now;
            Model.osobljeID = p.Id;
            Model.pacijentID = pacijentID;
            Model.pregledID = pregledID;
            Model.Cijena = ctx.Pregleds.Where(c => c.Id == Model.pregledID && c.PacijentId== pacijentID).FirstOrDefault().IzvrsenaUslugas.FirstOrDefault().Cijena;

            Session["Model"] = Model;
            return View(Model);
        }

        public ActionResult Snimi(RacunIzmijeni racun)
        {
            var Model = (RacunIzmijeni)Session["Model"];

            Racun racunDB;

            racunDB = new Racun();
            ctx.Racuns.Add(racunDB);

            racunDB.Cijena = ctx.Pregleds.Where(c=>c.Id==Model.pregledID).FirstOrDefault().IzvrsenaUslugas.FirstOrDefault().Cijena;
            racunDB.Uplaćeno = racun.Uplaćeno;
            racunDB.Datum = Model.Datum;
            racunDB.PacijentId = Model.pacijentID;
            racunDB.OsobljeId = Model.osobljeID;
            racunDB.PregledId = Model.pregledID;

            ctx.SaveChanges();


            return RedirectToAction("Index");

        }
    }
}