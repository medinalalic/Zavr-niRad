using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulOsoblje.Models;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Controllers
{
    [Autorizacija(KorisnickeUloge.Osoblje)]
    public class RacunController : Controller
    {
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        // GET: ModulOsoblje/Racun
        //public ActionResult Index(int id)
        //{
        //    List<RacunPrikaz.RacunInfo> racunInfo = ctx.Racun.Where(c=>c.PacijentId==id).Select(x => new RacunPrikaz.RacunInfo
        //    {
        //        Id = x.Id,
        //        Uplaćeno = x.Uplaćeno,
        //        Cijena = x.Cijena,
        //        Datum = x.Datum,
        //        Pacijent = ctx.Pacijent.Where(c => c.Id == id).FirstOrDefault().Korisnik.Ime + " " + ctx.Pacijent.Where(c => c.Id == id).FirstOrDefault().Korisnik.Prezime
        //    }).ToList();

        //    RacunPrikaz model = new RacunPrikaz
        //    {
        //        racun = racunInfo
        //    };
        //    model.p = ctx.Pacijent.Where(c => c.Id == id).Include(c=>c.Korisnik).FirstOrDefault().Korisnik.Ime;
        //    return View(model);

        //}
        public ActionResult IzdajRacun()
        {
            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Osoblje p = ctx.Osobljes.Where(x => x.Id == lp.Id).FirstOrDefault();

            RacunIzmijeni Model = new RacunIzmijeni();
            
            Model.Datum = DateTime.Now;
            Model.osobljeID = p.Id;
           // Model.pacijenti = UcitajPacijente();
            Session["Model"] = Model;
            return View(Model);
            
            
        }

        private List<SelectListItem> UcitajPacijente()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Value = null, Text = "Odaberite pacijenta" });
            lista.AddRange(ctx.Pacijents.Include(c => c.Korisnik).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Korisnik.Ime+" "+x.Korisnik.Prezime }).ToList());

            return lista;
        }
        public ActionResult Prikaz(string search)
        {RacunPrikaz model = new RacunPrikaz();
            List<RacunPrikaz.RacunInfo> racunInfo;
            if (search == "" || search==null)
            {
                racunInfo = ctx.Racuns
                    .Select(x => new RacunPrikaz.RacunInfo
                    {
                        Id = x.Id,
                        Uplaćeno = x.Uplaćeno,
                        Cijena = x.Cijena,
                        Datum = x.Datum,
                        Pacijent = ctx.Racuns.Where(c => c.Id == x.Id).FirstOrDefault().Pacijent.Korisnik.Ime + " " + ctx.Racuns.Where(c => c.Id == x.Id).FirstOrDefault().Pacijent.Korisnik.Prezime

                    }).ToList();
            }else
            {
                racunInfo = ctx.Racuns.Where(c=>c.Pacijent.Korisnik.Ime.ToLower().Contains(search.ToLower()) || c.Pacijent.Korisnik.Prezime.ToLower().Contains(search.ToLower()))

                   .Select(x => new RacunPrikaz.RacunInfo
                   {
                       Id = x.Id,
                       Uplaćeno = x.Uplaćeno,
                       Cijena = x.Cijena,
                       Datum = x.Datum,
                       Pacijent = ctx.Racuns.Where(c => c.Id == x.Id).FirstOrDefault().Pacijent.Korisnik.Ime + " " + ctx.Racuns.Where(c => c.Id == x.Id).FirstOrDefault().Pacijent.Korisnik.Prezime

                   }).ToList();
                
               
            }


            model.racun = racunInfo;
           
            return View(model);

        }

        public ActionResult Snimi(RacunIzmijeni racun)
        {
            var Model=(RacunIzmijeni)Session["Model"];

            Racun racunDB;
           
                   racunDB = new Racun();
                    ctx.Racuns.Add(racunDB);
            
         
                
               

                racunDB.Cijena = racun.Cijena;
                racunDB.Uplaćeno = racun.Uplaćeno;
                racunDB.Datum = Model.Datum;
                racunDB.PacijentId = racun.pacijentID;
                racunDB.OsobljeId = Model.osobljeID;
                ctx.SaveChanges();
             

                return RedirectToAction("Prikaz");

            }
        public ActionResult Izmijeni(int racunID)
        {
            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Osoblje p = ctx.Osobljes.Where(x => x.Id == lp.Id).FirstOrDefault();
            Racun r = ctx.Racuns
             .Where(x => x.Id == racunID).SingleOrDefault();

            RacunIzmijeni model = new RacunIzmijeni
            {
                Id = r.Id,
                  Uplaćeno = r.Uplaćeno,
                  Cijena = r.Cijena,
                  Datum = r.Datum,
                  osobljeID=p.Id,
                  pacijentID=r.PacijentId,
                  //pacijenti=UcitajPacijente(r.PacijentId)
             

            };
           
                 

            return View(model);


        }

        private List<SelectListItem> UcitajPacijente(int? pacijentId)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.AddRange(ctx.Pacijents.Where(c=>c.Id==pacijentId).Include(c => c.Korisnik).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Korisnik.Ime + " " + x.Korisnik.Prezime }).ToList());

            return lista;
        }
    }
    }
