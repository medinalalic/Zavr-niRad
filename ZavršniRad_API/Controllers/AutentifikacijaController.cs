using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZavršniRad_API.DAL;
using ZavršniRad_API.ViewModel;

namespace ZavršniRad_API.Controllers
{
    public class AutentifikacijaController : ApiController
    {
        private Stomatoloska_MLEntities ctx = new Stomatoloska_MLEntities();
        [HttpGet]
        [Route("api/Autentifikacija/Provjera/{username}/{lozinka}")]
        public KorisniciVM Provjera(string username, string lozinka)
        {

            KorisniciVM k = ctx.Korisniks.Where(x => x.KorisnickoIme == username && x.Lozinka == lozinka).
                Include(x=>x.Pacijent).Select(x => new KorisniciVM
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                KorisnickoIme = x.KorisnickoIme,
                Lozinka = x.Lozinka,
                Email = x.Email,
                Id = x.Id,
                Adresa = x.Adresa,
                Mobitel = x.Mobitel,
                Aktivan=x.Aktivan,
                IsAdmin=x.IsAdmin
                
            }).FirstOrDefault();
            return k;
        }
    }
}
