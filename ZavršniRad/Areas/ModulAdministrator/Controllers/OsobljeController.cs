
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ZavršniRad.Areas.ModulAdministrator.Models;
using ZavršniRad.Models;
using PagedList;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class OsobljeController : Controller
    {
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
        // GET: Osoblje
        public ActionResult Index(string search,string search2, int? osobljeid)
        {

            string s,s1;
            int o = osobljeid ?? 0;

            if (search == null) s = "";
            else s = search;
            if (search2 == null) s1 = "";
            else s1 = search2;
            List<Osoblje> op;
            if (s == "" && s1 == "" && o == 0) op = ctx.Osobljes.Where(c=>c.Korisnik.IsAdmin==false).Include(c => c.Korisnik).ToList();
            else if (s != "" || s1 != "" && o == 0) op = ctx.Osobljes.Where(c => c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower()) && c.Korisnik.IsAdmin==false).Include(c => c.Korisnik).ToList();
            else if (s == "" || s1 == "" && o != 0) op = ctx.Osobljes.Where(c => c.Id == o && c.Korisnik.IsAdmin == false).Include(c => c.Korisnik).ToList();
            else op = ctx.Osobljes.Where(c => c.Id == o && c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower()) && c.Korisnik.IsAdmin == false).Include(c => c.Korisnik).ToList();

            OsobljePagingVM model = new OsobljePagingVM { };
            model.OsobljeList = op;


            return View(model);
         
        }


        public ActionResult Dodaj()
        {
            OsobljeIzmijeniVM Model = new OsobljeIzmijeniVM();
          

            return View("Izmijeni", Model);
        }

        public ActionResult Deaktiviraj(int osobljeId)
        {
            Osoblje osobljeDB = ctx.Osobljes.Where(x => x.Id == osobljeId).Include(x => x.Korisnik).FirstOrDefault();


            osobljeDB.Korisnik.Aktivan = false;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }



        public ActionResult Obrisi(int OsobljeID)
        {
            Osoblje osoblje = ctx.Osobljes.Find(OsobljeID);
            Korisnik korisnik = ctx.Korisniks.Where(x => x.Id == OsobljeID).FirstOrDefault();
            ctx.Osobljes.Remove(osoblje);
            ctx.Korisniks.Remove(korisnik);

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Snimi(OsobljeIzmijeniVM osoblje)
        {
            if (!ModelState.IsValid)
            {
                

                return View("Izmijeni", osoblje);
            }
            else
            {
                Osoblje osobljeDB;
                if (osoblje.Id == 0)
                {
                    osobljeDB = new Osoblje();
                    osobljeDB.Korisnik = new Korisnik();
                    ctx.Osobljes.Add(osobljeDB);
                }
                else
                {
                    osobljeDB = ctx.Osobljes.Where(s => s.Id == osoblje.Id).Include(s => s.Korisnik).FirstOrDefault();
                }

                osobljeDB.Korisnik.Ime = osoblje.Ime;
                osobljeDB.Korisnik.Prezime = osoblje.Prezime;
                osobljeDB.Korisnik.Email = osoblje.Email;
                osobljeDB.Korisnik.Mobitel = osoblje.Mobitel;
                osobljeDB.Korisnik.Adresa = osoblje.Adresa;
                osobljeDB.Korisnik.KorisnickoIme = osoblje.KorisnickoIme;
                osobljeDB.JMBG = osoblje.JMBG;
                osobljeDB.Titula = osoblje.Titula;
                osobljeDB.Korisnik.IsAdmin = osoblje.IsAdmin;
                osobljeDB.Korisnik.Aktivan = true;
                osobljeDB.Korisnik.Lozinka = osoblje.Lozinka;

                osobljeDB.Korisnik.LozinkaSalt = UIHelper.GenerateSalt();
                osobljeDB.Korisnik.LozinkaHash = UIHelper.GenerateHash(osoblje.Lozinka, osobljeDB.Korisnik.LozinkaSalt);

                ctx.SaveChanges();

                return RedirectToAction("Index");
            }

        }
       
    }
}