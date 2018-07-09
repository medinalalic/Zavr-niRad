using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult TerminRazlogDatum(DateTime datum,string razlog)
        {
            
                SlobodniVM model = new SlobodniVM();
                Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
                List<string> SlobodnihTermina = new List<string>();

                List<string> satnice = new List<string> { "09:00", "09:30", "10:00","10:30",
            "11:00","11:30","12:00","12:30","13:00","15:00","15:30","16:00","16:30"};

                var terminis = satnice
                  .Select(i =>
                  {
                      TimeSpan result;
                      if (TimeSpan.TryParse(i, out result))
                          return new Nullable<TimeSpan>(result);
                      return null;
                  })
                  .Where(x => x.HasValue)
                  .ToList();




                if (terminis != null)
                {
                    foreach (var x in terminis)
                    {

                        datum = datum.Date + x.Value;
                        DateTime vrijeme = datum.Date + x.Value;
                        if (slobodan(datum))
                        {
                            SlobodnihTermina.Add(x.ToString());
                        }
                    }

                }
                model.Razlog = razlog;
                model.satnice = SlobodnihTermina;
                model.PacijentId = k.Id;
                model.Datum = datum;
                return View(model);
               
        }

        public bool slobodan(DateTime datum)
        {
            int b = ctx.Termins.Where(a => datum.Day == a.Vrijeme.Day && datum.Month == a.Vrijeme.Month
                      && datum.Year == a.Vrijeme.Year && datum.Hour == a.Vrijeme.Hour
                      && datum.Minute == a.Vrijeme.Minute).Count();


            if (b == 0)
                return true;
            else
                return false;
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

        public ActionResult Snimi(DateTime datum,string razlog,string vrijeme)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Termin terminDB;
          
                    terminDB = new Termin();
                    ctx.Termins.Add(terminDB);

            TimeSpan time = TimeSpan.Parse(vrijeme);
            
            TimeSpan ts = new TimeSpan(time.Ticks);
            datum = datum.Date + ts;

            terminDB.RazlogPosjete = razlog;
                terminDB.Datum =datum;
                terminDB.Odobren = false;
                terminDB.Vrijeme = datum;
                terminDB.PacijentId = k.Pacijent.Id;
                 ctx.Termins.Add(terminDB);
               
                ctx.SaveChanges();
                return RedirectToAction("Index");



            }



        //public ActionResult Snimi(SlobodniVM termin)
        //{
        //    Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
        //    Termin terminDB;

        //    if (termin.Id == 0)
        //    {
        //        terminDB = new Termin();
        //        ctx.Termins.Add(terminDB);
        //    }
        //    else
        //    {
        //        terminDB = ctx.Termins.Find(termin.Id);
        //    }



        //    terminDB.RazlogPosjete = termin.Razlog;
        //    terminDB.Datum = termin.Datum;
        //    terminDB.Odobren = false;
        //    terminDB.Vrijeme = termin.Vrijeme;
        //    terminDB.PacijentId = k.Pacijent.Id;
        //    var lista = ctx.Termins.Where(a => a.Datum >= DateTime.Today);

        //    foreach (var x in lista)
        //    {
        //        if (terminDB.Datum.Date == x.Datum.Date)
        //        {
        //            if (terminDB.Vrijeme.TimeOfDay == x.Vrijeme.TimeOfDay)
        //            {
        //                TempData["TerminZauzet"] = "Žao nam je taj termin je zauzet!";

        //                return RedirectToAction("Dodaj");
        //            }
        //            else
        //            {
        //                ctx.Termins.Add(terminDB);
        //            }
        //        }
        //        else
        //        {
        //            ctx.Termins.Add(terminDB);
        //        }

        //    }
        //    ctx.SaveChanges();
        //    return RedirectToAction("Index");



        //}

    }
}