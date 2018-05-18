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

namespace ZavršniRad_API.Controllers
{
    public class PorukaController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Poruka
        public IQueryable<Poruka> GetPorukas()
        {
            return db.Porukas;
        }

        // GET: api/Poruka/5
        [ResponseType(typeof(Poruka))]
        public IHttpActionResult GetPoruka(int id)
        {
            Poruka poruka = db.Porukas.Find(id);
            if (poruka == null)
            {
                return NotFound();
            }

            return Ok(poruka);
        }

        // PUT: api/Poruka/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoruka(int id, Poruka poruka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poruka.Id)
            {
                return BadRequest();
            }

            db.Entry(poruka).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PorukaExists(id))
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

        // POST: api/Poruka
        [ResponseType(typeof(Poruka))]
        public IHttpActionResult PostPoruka(Poruka poruka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Porukas.Add(poruka);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = poruka.Id }, poruka);
        }

        // DELETE: api/Poruka/5
        [ResponseType(typeof(Poruka))]
        public IHttpActionResult DeletePoruka(int id)
        {
            Poruka poruka = db.Porukas.Find(id);
            if (poruka == null)
            {
                return NotFound();
            }

            db.Porukas.Remove(poruka);
            db.SaveChanges();

            return Ok(poruka);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PorukaExists(int id)
        {
            return db.Porukas.Count(e => e.Id == id) > 0;
        }
    }
}