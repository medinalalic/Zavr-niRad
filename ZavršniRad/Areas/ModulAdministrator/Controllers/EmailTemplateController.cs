using System;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using ZavršniRad.Areas.ModulAdministrator.Models;
using ZavršniRad.Helper;

namespace ZavršniRad.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class EmailTemplateController : Controller
    {
        // GET: ModulAdministrator/EmailTemplate
        Stomatoloska_MLEntities1 ctx = new Stomatoloska_MLEntities1();
       public static async Task<string> EmailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
            StreamReader obj = new StreamReader(templateFilePath);
            var body = await obj.ReadToEndAsync();
            obj.Close();
            return body;
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult SendEmail(int pacijentId,string email,string ime,string user,string loz)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendEmail(SendEmail model)
        {
            var message = await EmailTemplate("TempEmail");
            message = message.Replace("Naslov neki", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Email));
            var pacijent = ctx.Pacijents.Where(x => x.Id == model.pacijentId).Include(x => x.Korisnik).FirstOrDefault();
            model.Email = pacijent.Korisnik.Email;
            var aktiv = ctx.Pacijents.Include(c => c.Korisnik).Where(c => c.Id == model.pacijentId).First();
            aktiv.Korisnik.Aktivan = true;
            ctx.SaveChanges();
            await EmailServices.SendEmailAsync(pacijent.Korisnik.Email, "Dobro došli", message);

            return View("EmailSent");
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult EmailSent()
        {
            return View();
        }

       
    }
}