using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class HomeAController : Controller
    {
        // GET: ModulAdministrator/HomeA
        public ActionResult Index()
        {
            return View();
        }
    }
}