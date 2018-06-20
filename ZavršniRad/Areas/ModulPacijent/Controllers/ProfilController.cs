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

    public class ProfilController : Controller
    {
        // GET: ModulPacijent/Profil
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        
       
        public ActionResult IzmjenaPristupnihPodataka()
        {
            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Pacijent p = ctx.Pacijents.Where(x => x.Id == lp.Id).FirstOrDefault();

            var Model = new UrediProfilVM();

            Model.Lozinka = lp.Lozinka;

            return View(Model);
        }

        public ActionResult SnimiPristupniPodaci(UrediProfilVM model)
        {
            if (!ModelState.IsValid)
            {

                return View("IzmjenaPristupnihPodataka", model);
            }

            Korisnik lp = (Korisnik)ControllerContext.HttpContext.Session["logirani_korisnik"];
            Pacijent DBPacijent = ctx.Pacijents.Where(x => x.Id == lp.Id).Include(x=>x.Korisnik).FirstOrDefault();

            DBPacijent.Korisnik.Lozinka = model.Lozinka;
            DBPacijent.Korisnik.LozinkaSalt = UIHelper.GenerateSalt();
            DBPacijent.Korisnik.LozinkaHash = UIHelper.GenerateHash(model.Lozinka, DBPacijent.Korisnik.LozinkaSalt);

            ctx.SaveChanges();
            TempData["Success"] = "Uspijesno sačuvano !";
            return RedirectToAction("IzmjenaPristupnihPodataka");
        }
    }
}