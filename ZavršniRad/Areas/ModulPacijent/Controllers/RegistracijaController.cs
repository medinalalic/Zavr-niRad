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

    public class RegistracijaController : Controller
    {
        // GET: ModulPacijent/Registracija
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
      
        public ActionResult Dodaj(RegistracijaVM model)
        {
            Pacijent DBpacijent;
            if (model.Id == 0)
            {

                DBpacijent = new Pacijent() {
                    JMBG = "1234567891245",
                };
                DBpacijent.Korisnik = new Korisnik();
            }
            else
            {
                DBpacijent = ctx.Pacijents.Where(x => x.Id == model.Id).Include(x => x.Korisnik).FirstOrDefault();
            }
            DBpacijent.Id = model.Id;
            DBpacijent.Korisnik.Id = model.Id;
            DBpacijent.Korisnik.Ime = model.Ime;
            DBpacijent.Korisnik.Prezime = model.Prezime;
            DBpacijent.Korisnik.Email = model.Email;
            DBpacijent.AddedOn = DateTime.Now;
            DBpacijent.Korisnik.Aktivan = false;
            DBpacijent.Korisnik.IsAdmin = false;
            DBpacijent.Korisnik.Mobitel = "061000000";
            DBpacijent.Korisnik.Adresa = "Potoci bb";
            DBpacijent.Korisnik.KorisnickoIme = model.KorisnickoIme;
            DBpacijent.Korisnik.Lozinka = model.Lozinka;
            DBpacijent.Korisnik.LozinkaSalt = UIHelper.GenerateSalt();
            DBpacijent.Korisnik.LozinkaHash = UIHelper.GenerateHash(model.Lozinka, DBpacijent.Korisnik.LozinkaSalt);
          
            ctx.Pacijents.Add(DBpacijent);
            ctx.SaveChanges();
            Session["logirani_pacijent"] = DBpacijent.Id;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}