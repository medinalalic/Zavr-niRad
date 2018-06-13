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
    public class DijagnozaController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Dijagnoza
        public IQueryable<Dijagnoza> GetDijagnozas()
        {
            return db.Dijagnozas;
        }

        // GET: api/Dijagnoza/5
        [ResponseType(typeof(Dijagnoza))]
        public IHttpActionResult GetDijagnoza(int id)
        {
            Dijagnoza dijagnoza = db.Dijagnozas.Find(id);
            if (dijagnoza == null)
            {
                return NotFound();
            }

            return Ok(dijagnoza);
        }
        [HttpGet]
        [Route("api/Dijagnoza/GetDijagnozaDrop")]
        public DijagnozaDropVM GetUslugeDrop()
        {

            DijagnozaDropVM model = new DijagnozaDropVM();
            model.dijagnoze = db.Dijagnozas.Select(x => new DijagnozaDropVM.DijagnozaInfo
            {
                Id = x.Id,
                Naziv = x.Naziv,


            }).ToList();
            return model;

        }
        // PUT: api/Dijagnoza/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDijagnoza(int id, Dijagnoza dijagnoza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dijagnoza.Id)
            {
                return BadRequest();
            }

            db.Entry(dijagnoza).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DijagnozaExists(id))
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

        // POST: api/Dijagnoza
        [ResponseType(typeof(Dijagnoza))]
        public IHttpActionResult PostDijagnoza(Dijagnoza dijagnoza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dijagnozas.Add(dijagnoza);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dijagnoza.Id }, dijagnoza);
        }

        // DELETE: api/Dijagnoza/5
        [ResponseType(typeof(Dijagnoza))]
        public IHttpActionResult DeleteDijagnoza(int id)
        {
            Dijagnoza dijagnoza = db.Dijagnozas.Find(id);
            if (dijagnoza == null)
            {
                return NotFound();
            }

            db.Dijagnozas.Remove(dijagnoza);
            db.SaveChanges();

            return Ok(dijagnoza);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DijagnozaExists(int id)
        {
            return db.Dijagnozas.Count(e => e.Id == id) > 0;
        }
    }
}