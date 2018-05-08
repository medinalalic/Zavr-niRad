using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
            
            
        }

        public ActionResult Pocetna()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k == null)
                return RedirectToAction("Logout", "Login", new { area = "" });
            if (k.Pacijent != null)
                return RedirectToAction("Index", "HomeP", new { area = "ModulPacijent" });
            if (k.Stomatolog != null)
                return RedirectToAction("Index", "HomeS", new { area = "ModulStomatolog" });
            if (k.Osoblje != null && k.IsAdmin == true)
                return RedirectToAction("Index", "HomeA", new { area = "ModulAdministrator" });
            if (k.Osoblje != null)
                return RedirectToAction("Index", "HomeO", new { area = "ModulOsoblje" });

            return RedirectToAction("Logout", "Login", new { area = "" });

        }
        public ActionResult Onama()
        {
            return View();
        }

        public ActionResult Usluge()
        {
            return View();
        }

        public ActionResult Savjeti()
        {
            return View();
        }
    }
}