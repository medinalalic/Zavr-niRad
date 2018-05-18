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
    public class PacijentController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Pacijent
        public IQueryable<Pacijent> GetPacijents()
        {
            return db.Pacijents;
        }

        // GET: api/Pacijent/5
        [ResponseType(typeof(Pacijent))]
        public IHttpActionResult GetPacijent(int id)
        {
            Pacijent pacijent = db.Pacijents.Find(id);
            if (pacijent == null)
            {
                return NotFound();
            }

            return Ok(pacijent);
        }

        // PUT: api/Pacijent/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPacijent(Pacijent pacijent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(pacijent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpGet]
        [Route("api/Pacijent/GetPacijentByIme/{ime?}")]
        public List<usp_Pacijent_ByIme_Result> GetUslugeByNaziv(string ime = "")
        {
            return db.usp_Pacijent_ByIme(ime).ToList();
        }

      

        [HttpGet]
        [Route("api/Pacijent/GetAllPacijenti")]
        public List<usp_PacijentiAll_Result> GetAllPacijenti()
        {

            return db.usp_PacijentiAll().ToList();
        }
        // POST: api/Pacijent
        [ResponseType(typeof(Pacijent))]
        public IHttpActionResult PostPacijent(Pacijent pacijent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacijents.Add(pacijent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PacijentExists(pacijent.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pacijent.Id }, pacijent);
        }

        // DELETE: api/Pacijent/5
        [ResponseType(typeof(Pacijent))]
        public IHttpActionResult DeletePacijent(int id)
        {
            Pacijent pacijent = db.Pacijents.Find(id);
            if (pacijent == null)
            {
                return NotFound();
            }

            db.Pacijents.Remove(pacijent);
            db.SaveChanges();

            return Ok(pacijent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacijentExists(int id)
        {
            return db.Pacijents.Count(e => e.Id == id) > 0;
        }
    }
}