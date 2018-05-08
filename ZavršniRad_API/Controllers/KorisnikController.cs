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

        // PUT: api/Korisnik/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKorisnik(int id, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.Id)
            {
                return BadRequest();
            }

            db.Entry(korisnik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
      
        // POST: api/Korisnik
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult PostKorisnik(Korisnik korisnik,Pacijent pacijent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            
            db.Korisniks.Add(korisnik);
            pacijent.Id = korisnik.Id;
            db.Pacijents.Add(pacijent);
            db.SaveChanges();

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