using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulOsoblje.Models;
using ZavršniRad.Helper;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Controllers
{
    [Autorizacija(KorisnickeUloge.Osoblje)]

    public class MaterijalController : Controller
    {
        // GET: ModulOsoblje/Materijal
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
      
        public ActionResult Index(int? page, string search, int? matid)
        {
              Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            string s;
            int o = matid ?? 0;

            if (search == null) s = "";
            else s = search;
          
            IPagedList<PotroseniMaterijal> op;
            if (s == "" && o == 0) op = ctx.PotroseniMaterijals.ToList().ToPagedList(page ?? 1, 3);
            else if (s != "" && o == 0) op = ctx.PotroseniMaterijals.Where(c => c.Materijal.Naziv.ToLower().Contains(s.ToLower())).ToList().ToPagedList(page ?? 1, 3);
            else if (s == "" && o != 0) op = ctx.PotroseniMaterijals.Where(c => c.Id == o).ToList().ToPagedList(page ?? 1, 3);
            else op = ctx.PotroseniMaterijals.Where(c => c.Id == o && c.Materijal.Naziv.ToLower().Contains(s.ToLower())).ToList().ToPagedList(page ?? 1, 3);


            MaterijalPrikazVM model = new MaterijalPrikazVM {};
            model.ListaPotrošenih = op;


            return View(model);
        }
        public ActionResult Dodaj()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            MaterijalzmijeniVM Model = new MaterijalzmijeniVM();
            Model.materijal = UcitajMaterijale();
            Model.OsobljeId = k.Osoblje.Id;
            Model.Datum = DateTime.Now;
            return View("Dodaj", Model);
        }
        private List<SelectListItem> UcitajMaterijale()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Value = null, Text = "Odaberite materijal" });
            lista.AddRange(ctx.Materijals.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());

            return lista;
        }

        public ActionResult Snimi(MaterijalzmijeniVM stavke)
        {
            PotroseniMaterijal matDB;
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (!ModelState.IsValid)
            {
                return View("Izmijeni", stavke);
            }
            else
            {

                if (stavke.Id == 0)
                {
                    matDB = new PotroseniMaterijal();
                    ctx.PotroseniMaterijals.Add(matDB);
                }
                else
                {
                    matDB = ctx.PotroseniMaterijals.Find(stavke.Id);

                }
                matDB.Kolicina = stavke.Kolicina;
                matDB.Datum = stavke.Datum;
                matDB.OsobljeId = k.Osoblje.Id;
                matDB.MaterijalId = stavke.MaterijalId.Value;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Obrisi(int MaterijalId)
        {
            PotroseniMaterijal p = ctx.PotroseniMaterijals.Find(MaterijalId);

            if (p != null)
            {
                ctx.PotroseniMaterijals.Remove(p);
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");


        }

     

        public ActionResult Izmijeni(int MaterijalId)
        {
            PotroseniMaterijal mat = ctx.PotroseniMaterijals
             .Where(x => x.Id == MaterijalId).SingleOrDefault();

            MaterijalzmijeniVM model = new MaterijalzmijeniVM
            {
                Id = mat.Id,
                Kolicina = mat.Kolicina,
                Datum = mat.Datum,
                MaterijalId = mat.MaterijalId,
                materijal =UcitajMaterijale(mat.MaterijalId),
                OsobljeId = mat.OsobljeId

            };

            return View("Izmijeni",model);

         


        }

        private List<SelectListItem> UcitajMaterijale(int? materijalId)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.AddRange(ctx.Materijals.Where(c=>c.Id== materijalId).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());

            return lista;

        }
    }
}