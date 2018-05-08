using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulOsoblje.Controllers
{
    [Autorizacija(KorisnickeUloge.Osoblje)]

    public class HomeOController : Controller
    {
        // GET: ModulOsoblje/HomeO
        public ActionResult Index()
        {
            return View();
        }
    }
}