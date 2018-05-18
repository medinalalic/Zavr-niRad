using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulPacijent.Models;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulPacijent.Controllers
{
    [Autorizacija(KorisnickeUloge.Pacijent)]

    public class ZakaziTerminController : Controller
    {
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        // GET: ModulPacijent/ZakaziTermin
        //public ActionResult Index()
        //{
        //    Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

        //    ZakaziTerminVM Model = new ZakaziTerminVM();
        //    Model.termin = ctx.Termin.Where(x => x.PacijentId == k.Pacijent.Id)
        //           .Select(x => new ZakaziTerminVM.TerminInfo
        //        {
        //            Id = x.Id,
        //            Razlog = x.RazlogPosjete,
        //            Odobren = x.Odobren,
        //            Datum = x.Datum,
        //            Vrijeme = x.Vrijeme,
        //            Ime = k.Pacijent.Korisnik.Ime,
        //            Prezime = k.Pacijent.Korisnik.Prezime,
        //            PacijentId=k.Pacijent.Id

        //        }).ToList();
            
        //    return View(Model);


        //}
        public ActionResult Index(int? page, DateTime? from, DateTime? to)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);


            int o = k.Pacijent.Id;
            IPagedList<Termin> op;
            if (from != null && to != null) op = ctx.Termins.Where(c => c.PacijentId == o && c.Datum >= from && c.Datum <= to).ToList().ToPagedList(page ?? 1, 3);
            else if (from != null || to != null) op = ctx.Termins.Where(c => c.PacijentId == o && (c.Datum >= from || c.Datum <= to)).ToList().ToPagedList(page ?? 1, 3);
            else op = ctx.Termins.Where(c => c.PacijentId == o).ToList().ToPagedList(page ?? 1, 3);

            TerminListaVM model = new TerminListaVM { };
            model.TerminList = op;


            return View(model);
        }
        public ActionResult Dodaj()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Pacijent pacijent = ctx.Pacijents.Find(k.Pacijent.Id);

            TerminIzmijeniVM model = new TerminIzmijeniVM
            {
               
                PacijentId = k.Pacijent.Id

            };

            return View("Dodaj", model);

        }

        public ActionResult ZauzetiTermini(int? page)
        {
            IPagedList<Termin> op;
             op = ctx.Termins.Where(c=>c.Odobren==true && c.Datum>=DateTime.Today).ToList().ToPagedList(page ?? 1, 3);
            TerminListaVM model = new TerminListaVM { };
            model.TerminList = op;


            return View(model);
        }

        public ActionResult Obrisi(int TerminID)
        {
            Termin termin = ctx.Termins.Find(TerminID);
            if ((termin.Datum == DateTime.Now.Date) && termin.Odobren==false && (termin.Vrijeme.Hour > DateTime.Now.Hour))
            {
                ctx.Termins.Remove(termin);
                ctx.SaveChanges();
            }
            if ((termin.Datum > DateTime.Now.Date) && termin.Odobren == false)
            {
                ctx.Termins.Remove(termin);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Snimi(TerminIzmijeniVM termin)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Termin terminDB;
            if (!ModelState.IsValid)
            {
               return PartialView("Dodaj", termin);
            }
            else
            {
                if (termin.Id == 0)
                {
                    terminDB = new Termin();
                    ctx.Termins.Add(terminDB);
                }
                else
                {
                    terminDB = ctx.Termins.Find(termin.Id);
                }



                terminDB.RazlogPosjete = termin.Razlog;
                terminDB.Datum = termin.Datum;
                terminDB.Odobren = false;
                terminDB.Vrijeme = termin.Vrijeme;
                terminDB.PacijentId = k.Pacijent.Id;
                var lista = ctx.Termins.Where(a=>a.Datum >= DateTime.Today);
             
                foreach (var x in lista)
                {
                    if (terminDB.Datum == x.Datum)
                    {
                        if (terminDB.Vrijeme == x.Vrijeme)
                        {
                            TempData["TerminZauzet"] = "Žao nam je taj termin je zauzet!";

                            return RedirectToAction("Dodaj");
                       }
                        else
                        {
                            ctx.Termins.Add(terminDB);
                        }
                    }
                    else
                    {
                        ctx.Termins.Add(terminDB);
                    }
                 
                }
                ctx.SaveChanges();
                return RedirectToAction("Index");



            }
        }

     

       
    }
}