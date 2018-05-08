﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulStomatolog.Models;
using ZavršniRad.Models;
using PagedList;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulStomatolog.Controllers
{
    [Autorizacija(KorisnickeUloge.Stomatolog)]
    public class PacijentController : Controller
    {
        // GET: ModulStomatolog/Pacijent
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
      
        public ActionResult Index(int? page, string search, string search2, int? pacijentid)
        {

            string s, s1;
            int o = pacijentid ?? 0;

            if (search == null) s = "";
            else s = search;
            if (search2 == null) s1 = "";
            else s1 = search2;
            IPagedList<Pacijent> op;
            if (s == "" && s1 == "" && o == 0) op = ctx.Pacijents.Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else if (s != "" || s1 != "" && o == 0) op = ctx.Pacijents.Where(c => c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower())).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else if (s == "" || s1 == "" && o != 0) op = ctx.Pacijents.Where(c => c.Id == o).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);
            else op = ctx.Pacijents.Where(c => c.Id == o && c.Korisnik.Ime.ToLower().Contains(s.ToLower()) || c.Korisnik.Prezime.ToLower().Contains(s1.ToLower())).Include(c => c.Korisnik).ToList().ToPagedList(page ?? 1, 3);

            PacijentPagingVM model = new PacijentPagingVM { };
            model.PacijentList = op;


            return View(model);

        }

    }
}