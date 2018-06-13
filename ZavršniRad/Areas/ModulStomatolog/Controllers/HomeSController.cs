using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulStomatolog.Models;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulStomatolog.Controllers
{
    [Autorizacija(KorisnickeUloge.Stomatolog)]

    public class HomeSController : Controller
    {
        // GET: ModulStomatolog/Home
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        public ActionResult Index()
        {
            var day1 = DateTime.Now;
            ZakazaniTermini Model = new ZakazaniTermini();
            Model.termin = ctx.Termins.ToList().Where(x => x.Datum.Year.Equals(day1.Year) && x.Datum.Month.Equals(day1.Month) && x.Datum.Day.Equals(day1.Day))
              
               .Select(x => new ZakazaniTermini.TerminInfo
               {
                   Id = x.Id,
                   Datum = x.Datum,
                   Vrijeme = x.Vrijeme,
                   Razlog = x.RazlogPosjete,
                   Odobren = x.Odobren,
                   PacijentId = x.PacijentId,
                   Pacijent = ctx.Pacijents.Include(s => s.Korisnik).Where(s => s.Id == x.PacijentId).FirstOrDefault().Korisnik.Ime + " " + ctx.Pacijents.Include(s => s.Korisnik).Where(s => s.Id == x.PacijentId).FirstOrDefault().Korisnik.Prezime,
                   Obavljen = PregledObavljen(x.Id, x.PacijentId)
               }).ToList();
            
            return View(Model);
        }
        private bool PregledObavljen(int terminID,int pacijentID)
        {
            Pregled c = ctx.Pregleds.Where(p => p.PacijentId==pacijentID && p.TerminId==terminID).FirstOrDefault();
            if (c !=null && c.IsObavljen==true)
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