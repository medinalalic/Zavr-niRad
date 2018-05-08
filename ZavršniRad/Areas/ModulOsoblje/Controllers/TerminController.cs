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
            var subota = petak.AddDays(1);


            model.Pocetak = startDate;
            model.Kraj = endDate;
            model.Da = startDate.ToString();
            model._Ponedjeljak = ctx.Termins
                .Where(x => x.Datum >= dat && x.Datum <= dat && x.Vrijeme != null)
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
               .Where(x => x.Datum >= utorak && x.Datum <= utorak && x.Vrijeme != null)
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
              .Where(x => x.Datum >= srijeda && x.Datum <= srijeda && x.Vrijeme != null)
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
              .Where(x => x.Datum >= cetvrtak && x.Datum <= cetvrtak && x.Vrijeme != null)
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
              .Where(x => x.Datum >= petak && x.Datum <= petak && x.Vrijeme != null)
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
            return RedirectToAction("Index");
        }

       
    }
}