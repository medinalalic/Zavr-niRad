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

    public class ProfilController : Controller
    {
        // GET: ModulOsoblje/Profil

        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        public ActionResult IzmjenaPristupnihPodataka()
        {
            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Osoblje p = ctx.Osobljes.Where(x => x.Id == lp.Id).FirstOrDefault();

            var Model = new UrediProfilVM();

            Model.Lozinka = lp.Lozinka;

            return View(Model);
        }

        public ActionResult SnimiPristupniPodaci(UrediProfilVM model)
        {

            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Osoblje DBPacijent = ctx.Osobljes.Where(x => x.Id == lp.Id).Include(x => x.Korisnik).FirstOrDefault();

            DBPacijent.Korisnik.Lozinka = model.Lozinka;
            DBPacijent.Korisnik.LozinkaSalt = UIHelper.GenerateSalt();
            DBPacijent.Korisnik.LozinkaHash = UIHelper.GenerateHash(model.Lozinka, DBPacijent.Korisnik.LozinkaSalt);

            ctx.SaveChanges();
            TempData["Success"] = "Uspješno sačuvano !";
            return RedirectToAction("IzmjenaPristupnihPodataka");
        }
    }
}