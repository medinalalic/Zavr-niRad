using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulStomatolog.Models;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Controllers
{
    [Autorizacija(KorisnickeUloge.Osoblje)]

    public class TerminController : Controller
    {
        // GET: ModulOsoblje/Termin
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
      

        public static DateTime PosljedniDanSedmice(DateTime date)
        {
            DateTime ldowDate = PrviDanSedmice(date).AddDays(6);
            return ldowDate;
        }

        public static DateTime PrviDanSedmice(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }
       

        public ActionResult Rezervacije()
        {
            var model = new AktivniTerminiVM();
          
            var startDate = PrviDanSedmice(DateTime.Now);
            var endDate = PosljedniDanSedmice(DateTime.Now);

            var startFormat = startDate.ToString("yyy-MM-dd");
            var dat = Convert.ToDateTime(startFormat);

            var EndFormat = endDate.ToString("yyy-MM-dd");
            var datEnd = Convert.ToDateTime(EndFormat);

            var utorak = dat.AddDays(1);
            var srijeda = utorak.AddDays(1);
            var cetvrtak = srijeda.AddDays(1);
            var petak = cetvrtak.AddDays(1);


            model.Pocetak = startDate;
            model.Kraj = endDate;
            model.Pon= startDate.ToString("dd-MM-yyyy");
            model.Uto = utorak.ToString("dd-MM-yyyy");
            model.Sri = srijeda.ToString("dd-MM-yyyy");
            model.Cet = cetvrtak.ToString("dd-MM-yyyy");
            model.Pet = petak.ToString("dd-MM-yyyy");

            model.Da = startDate.ToString();
            model._Ponedjeljak = ctx.Termins
                .Where(x =>  x.Datum.Year.Equals(dat.Year) && x.Datum.Month.Equals(dat.Month) && x.Datum.Day.Equals(dat.Day))
                .Select(
                a => new AktivniTerminiVM.Ponedjeljak
                {
                    Id = a.Id,
                    Vrijeme = ctx.Termins.ToList().FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                    Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime +" "+ ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,Odobren=a.Odobren,
                    PacijentId=a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();
            

            model._Utorak = ctx.Termins
               .Where(x => x.Datum.Year.Equals(utorak.Year) && x.Datum.Month.Equals(utorak.Month) && x.Datum.Day.Equals(utorak.Day))
               .Select(
               a => new AktivniTerminiVM.Utorak
               {
                   Id = a.Id,
                   Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                   Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                   PacijentId =a.PacijentId,
                   Odobren = a.Odobren,
                   Datum = a.Datum,
                   Napomena = a.RazlogPosjete
               }).ToList();

            model._Srijeda = ctx.Termins
              .Where( x => x.Datum.Year.Equals(srijeda.Year) && x.Datum.Month.Equals(srijeda.Month) && x.Datum.Day.Equals(srijeda.Day))
              .Select(
              a => new AktivniTerminiVM.Srijeda
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId =a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();
            model._Cetvrtak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(cetvrtak.Year) && x.Datum.Month.Equals(cetvrtak.Month) && x.Datum.Day.Equals(cetvrtak.Day))
              .Select(
              a => new AktivniTerminiVM.Cetvrtak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId =a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();


            model._Petak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(petak.Year) && x.Datum.Month.Equals(petak.Month) && x.Datum.Day.Equals(petak.Day))
              .Select(
              a => new AktivniTerminiVM.Petak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId =a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();
            

           

            return View(model);


        }
        private bool PregledObavljen(int terminID)
        {
            
            int c = ctx.Pregleds.Where(p => p.TerminId == terminID).Select(p => p.Id).FirstOrDefault();
            if (c != 0)
                return true;
            return false;
        }
        public ActionResult Metoda(int id)
        {
            Termin p = ctx.Termins.Find(id);
            p.Odobren = true;
            ctx.SaveChanges();
            return RedirectToAction("Rezervacije");
        }



        public static DateTime PrviDanNaredneSedmice(DateTime date)
        {
            var nowdate = date.AddDays(7);
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }

        public ActionResult NextRezervacije()
        {

            var model = new AktivniTerminiVM();
            DateTime da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date);

            var endDate = PosljedniDanSedmice(date);

            var startFormat = startDate.ToString("yyy-MM-dd");
            var dat = Convert.ToDateTime(startFormat);

            var EndFormat = endDate.ToString("yyy-MM-dd");
            var datEnd = Convert.ToDateTime(EndFormat);

            var utorak = dat.AddDays(1);
            var srijeda = utorak.AddDays(1);
            var cetvrtak = srijeda.AddDays(1);
            var petak = cetvrtak.AddDays(1);

            model.Pon = startDate.ToString("dd-MM-yyyy");
            model.Uto = utorak.ToString("dd-MM-yyyy");
            model.Sri = srijeda.ToString("dd-MM-yyyy");
            model.Cet = cetvrtak.ToString("dd-MM-yyyy");
            model.Pet = petak.ToString("dd-MM-yyyy");


            model.Pocetak = startDate;
            model.Kraj = endDate;
            model.Da = startDate.ToString();
            model._Ponedjeljak = ctx.Termins
                .Where(x => x.Datum.Year.Equals(dat.Year) && x.Datum.Month.Equals(dat.Month) && x.Datum.Day.Equals(dat.Day))
                .Select(
                a => new AktivniTerminiVM.Ponedjeljak
                {
                    Id = a.Id,
                    Vrijeme = ctx.Termins.ToList().FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                    Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();


            model._Utorak = ctx.Termins
               .Where(x => x.Datum.Year.Equals(utorak.Year) && x.Datum.Month.Equals(utorak.Month) && x.Datum.Day.Equals(utorak.Day))
               .Select(
               a => new AktivniTerminiVM.Utorak
               {
                   Id = a.Id,
                   Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                   Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                   PacijentId = a.PacijentId,
                   Odobren = a.Odobren,
                   Datum = a.Datum,
                   Napomena = a.RazlogPosjete
               }).ToList();

            model._Srijeda = ctx.Termins
              .Where(x => x.Datum.Year.Equals(srijeda.Year) && x.Datum.Month.Equals(srijeda.Month) && x.Datum.Day.Equals(srijeda.Day))
              .Select(
              a => new AktivniTerminiVM.Srijeda
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();
            model._Cetvrtak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(cetvrtak.Year) && x.Datum.Month.Equals(cetvrtak.Month) && x.Datum.Day.Equals(cetvrtak.Day))
              .Select(
              a => new AktivniTerminiVM.Cetvrtak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();


            model._Petak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(petak.Year) && x.Datum.Month.Equals(petak.Month) && x.Datum.Day.Equals(petak.Day))
              .Select(
              a => new AktivniTerminiVM.Petak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();

           

            return View("Rezervacije", model);
        }

        public ActionResult BackRezervacije()
        {

            var model = new AktivniTerminiVM();
            DateTime da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date);

            var endDate = PosljedniDanSedmice(date);

            var startFormat = startDate.ToString("yyy-MM-dd");
            var dat = Convert.ToDateTime(startFormat);

            var EndFormat = endDate.ToString("yyy-MM-dd");
            var datEnd = Convert.ToDateTime(EndFormat);

            var utorak = dat.AddDays(1);
            var srijeda = utorak.AddDays(1);
            var cetvrtak = srijeda.AddDays(1);
            var petak = cetvrtak.AddDays(1);
            model.Pon = startDate.ToString("dd-MM-yyyy");
            model.Uto = utorak.ToString("dd-MM-yyyy");
            model.Sri = srijeda.ToString("dd-MM-yyyy");
            model.Cet = cetvrtak.ToString("dd-MM-yyyy");
            model.Pet = petak.ToString("dd-MM-yyyy");


            model.Pocetak = startDate;
            model.Kraj = endDate;
            model.Da = startDate.ToString();
            model._Ponedjeljak = ctx.Termins
                  .Where(x => x.Datum.Year.Equals(dat.Year) && x.Datum.Month.Equals(dat.Month) && x.Datum.Day.Equals(dat.Day))
                  .Select(
                  a => new AktivniTerminiVM.Ponedjeljak
                  {
                      Id = a.Id,
                      Vrijeme = ctx.Termins.ToList().FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                      Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                      ,
                      Odobren = a.Odobren,
                      PacijentId = a.PacijentId,
                      Datum = a.Datum,
                      Napomena = a.RazlogPosjete
                  }).ToList();


            model._Utorak = ctx.Termins
               .Where(x => x.Datum.Year.Equals(utorak.Year) && x.Datum.Month.Equals(utorak.Month) && x.Datum.Day.Equals(utorak.Day))
               .Select(
               a => new AktivniTerminiVM.Utorak
               {
                   Id = a.Id,
                   Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                   Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                   PacijentId = a.PacijentId,
                   Odobren = a.Odobren,
                   Datum = a.Datum,
                   Napomena = a.RazlogPosjete
               }).ToList();

            model._Srijeda = ctx.Termins
              .Where(x => x.Datum.Year.Equals(srijeda.Year) && x.Datum.Month.Equals(srijeda.Month) && x.Datum.Day.Equals(srijeda.Day))
              .Select(
              a => new AktivniTerminiVM.Srijeda
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();
            model._Cetvrtak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(cetvrtak.Year) && x.Datum.Month.Equals(cetvrtak.Month) && x.Datum.Day.Equals(cetvrtak.Day))
              .Select(
              a => new AktivniTerminiVM.Cetvrtak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();


            model._Petak = ctx.Termins
              .Where(x => x.Datum.Year.Equals(petak.Year) && x.Datum.Month.Equals(petak.Month) && x.Datum.Day.Equals(petak.Day))
              .Select(
              a => new AktivniTerminiVM.Petak
              {
                  Id = a.Id,
                  Vrijeme = ctx.Termins.FirstOrDefault(d => d.Id == a.Id).Vrijeme,
                  Pacijent = ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + ctx.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                  PacijentId = a.PacijentId,
                  Odobren = a.Odobren,
                  Datum = a.Datum,
                  Napomena = a.RazlogPosjete
              }).ToList();

            

            return View("Rezervacije", model);
        }
     
        public static DateTime PosljednjiDanNaredneSedmice(DateTime date)
        {
            DateTime ldowDate = PrviDanSedmice(date).AddDays(6);
            return ldowDate;
        }
    }
}