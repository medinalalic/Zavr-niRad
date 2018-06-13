using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZavršniRad_API;
using ZavršniRad_API.ViewModel;

namespace ZavršniRad_API.Controllers
{
    public class KorisnikController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Korisnik
        public IQueryable<Korisnik> GetKorisniks()
        {
            return db.Korisniks;
        }

        // GET: api/Korisnik/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult GetKorisnik(int id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }
        [HttpPost]
        [Route("api/Korisnik/IzmjenaPodatka")]
        public IHttpActionResult IzmjeniPodatke(KorisniciVM k)
        {
            Korisnik korisnik = new Korisnik();
            korisnik.Id = k.Id;
            korisnik.Ime = k.Ime;
            korisnik.Prezime = k.Prezime;
            korisnik.Lozinka = k.Lozinka;
            korisnik.KorisnickoIme = k.KorisnickoIme;
            korisnik.Email = k.Email;
            korisnik.Mobitel = k.Mobitel;
            korisnik.Aktivan = true;
            korisnik.Adresa = k.Adresa;
            
            db.Entry(korisnik).State = EntityState.Modified;
            db.SaveChanges();


            return Ok();
        }

        // PUT: api/Korisnik/5

        [ResponseType(typeof(void))]
        [Route("api/Korisnik/Put/{korisnik}")]

        public void Put([FromBody]Korisnik korisnik)
        {
            db.usp_KorisniciPut(korisnik.Id, korisnik.Ime, korisnik.Prezime, korisnik.Email,
                korisnik.Mobitel, korisnik.KorisnickoIme, korisnik.Lozinka, korisnik.Adresa);
        }

        // POST: api/Korisnik
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult PostKorisnik(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            korisnik.Id = Convert.ToInt32(db.esp_Korisnik_Insert(korisnik.Ime,korisnik.Prezime,
                korisnik.Email,korisnik.Mobitel,korisnik.Adresa,korisnik.KorisnickoIme,
                korisnik.Lozinka).FirstOrDefault());

           
                db.esp_Pacijents_Insert(korisnik.Id,null,DateTime.Now);
           

            return CreatedAtRoute("DefaultApi", new { id = korisnik.Id }, korisnik);
        }

        // DELETE: api/Korisnik/5
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult DeleteKorisnik(int id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.Korisniks.Remove(korisnik);
            db.SaveChanges();

            return Ok(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisnikExists(int id)
        {
            return db.Korisniks.Count(e => e.Id == id) > 0;
        }
    }
}