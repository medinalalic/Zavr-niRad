using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulPacijent.Controllers
{
    [Autorizacija(KorisnickeUloge.Pacijent)]

    public class HomePController : Controller
    {
        // GET: ModulPacijent/HomeP
        public ActionResult Index()
        {
            return View();
        }
    }
}