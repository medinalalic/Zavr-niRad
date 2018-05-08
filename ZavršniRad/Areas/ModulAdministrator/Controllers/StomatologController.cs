
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using ZavršniRad.Areas.ModulAdministrator.Models;
using ZavršniRad.Models;
using PagedList;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class StomatologController : Controller
    {
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();

        // GET: Stomatolog
        public ActionResult Dodaj()
        {
            StomatologIzmijeniVM Model = new StomatologIzmijeniVM();

            return View("Izmijeni", Model);
        }

        public ActionResult Index(string search,string search2, int? stomatologid)
        {

            string s,s1;
            int o = stomatologid ?? 0;

            if (search == null) s = "";
            else s = search;
            if (search2 == null) s1 = "";
            else s1 = search2;
            List<Stomatolog> op;
            if (s == "" && s1 == "" && o == 0) op = ctx.Stomatologs.Include(c => c.Korisnik).ToList();
            else if (s != "" || s1 != "" && o == 0) op = ctx.Stomatologs.Where(c => c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower())).Include(c => c.Korisnik).ToList();
            else if (s == "" || s1 == "" && o != 0) op = ctx.Stomatologs.Where(c => c.Id == o).Include(c => c.Korisnik).ToList();
            else op = ctx.Stomatologs.Where(c => c.Id == o && c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower())).Include(c => c.Korisnik).ToList();

            StomatologPagingVM model = new StomatologPagingVM { };
            model.StomatologList = op;


            return View(model);

        }



      
        public ActionResult Obrisi(int StomatologID)
        {
            Stomatolog stomatolog = ctx.Stomatologs.Find(StomatologID);
            Korisnik korisnik = ctx.Korisniks.Where(x => x.Id == StomatologID).FirstOrDefault();
            ctx.Stomatologs.Remove(stomatolog);
            ctx.Korisniks.Remove(korisnik);

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Deaktiviraj(int stomatologId)
        {
           Stomatolog stomatologDB = ctx.Stomatologs.Where(x => x.Id == stomatologId).Include(x => x.Korisnik).FirstOrDefault();


            stomatologDB.Korisnik.Aktivan = false;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Snimi(StomatologIzmijeniVM stomatolog)
        {


            if (!ModelState.IsValid)
            {
                return View("Izmijeni", stomatolog);
            }
            else
            {
                Stomatolog stomatologDB;
                if (stomatolog.Id == 0)
                {
                    stomatologDB = new Stomatolog();
                    stomatologDB.Korisnik = new Korisnik();
                    ctx.Stomatologs.Add(stomatologDB);
                }
                else
                {
                    stomatologDB = ctx.Stomatologs.Where(s => s.Id == stomatolog.Id).Include(s => s.Korisnik).FirstOrDefault();
                }

                stomatologDB.Korisnik.Ime = stomatolog.Ime;
                stomatologDB.Korisnik.Prezime = stomatolog.Prezime;
                stomatologDB.Korisnik.Email = stomatolog.Email;
                stomatologDB.Korisnik.Mobitel = stomatolog.Mobitel;
                stomatologDB.Korisnik.Adresa = stomatolog.Adresa;
                stomatologDB.Korisnik.KorisnickoIme = stomatolog.KorisnickoIme;
                stomatologDB.JMBG = stomatolog.JMBG;
                stomatologDB.Titula = stomatolog.Titula;
                stomatologDB.Korisnik.IsAdmin = stomatolog.IsAdmin;
                stomatologDB.Korisnik.Aktivan = true;
                stomatologDB.Korisnik.Lozinka = stomatolog.Lozinka;

                stomatologDB.Korisnik.LozinkaSalt = UIHelper.GenerateSalt();
                stomatologDB.Korisnik.LozinkaHash = UIHelper.GenerateHash(stomatolog.Lozinka, stomatologDB.Korisnik.LozinkaSalt);

                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
        }


    }
}