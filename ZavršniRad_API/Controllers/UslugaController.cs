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
    public class UslugaController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Usluga
        public IQueryable<Usluga> GetUslugas()
        {
            return db.Uslugas;
        }

        // GET: api/Usluga/5
        [ResponseType(typeof(Usluga))]
        public IHttpActionResult GetUsluga(int id)
        {
            Usluga usluga = db.Uslugas.Find(id);
            if (usluga == null)
            {
                return NotFound();
            }

            return Ok(usluga);
        }

        // PUT: api/Usluga/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsluga(int id, Usluga usluga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usluga.Id)
            {
                return BadRequest();
            }

            db.Entry(usluga).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UslugaExists(id))
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

        // POST: api/Usluga
        [ResponseType(typeof(Usluga))]
        public IHttpActionResult PostUsluga(Usluga usluga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Uslugas.Add(usluga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usluga.Id }, usluga);
        }

        [HttpGet]
        [Route("api/Usluga/GetUsluge/{naziv?}")]
        public UslugaVM GetUsluge(string naziv="")
        {

            UslugaVM model = new UslugaVM();
            model.UslugaLista = db.Uslugas.Where(x => x.Vrsta == naziv).Select(x => new UslugaVM.UslugaInfo
            {
                Id = x.Id,
                Vrsta = x.Vrsta,
                Slika = x.Slika

            }).ToList();
            return model;
        }


        // DELETE: api/Usluga/5
        [ResponseType(typeof(Usluga))]
        public IHttpActionResult DeleteUsluga(int id)
        {
            Usluga usluga = db.Uslugas.Find(id);
            if (usluga == null)
            {
                return NotFound();
            }

            db.Uslugas.Remove(usluga);
            db.SaveChanges();

            return Ok(usluga);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UslugaExists(int id)
        {
            return db.Uslugas.Count(e => e.Id == id) > 0;
        }
    }
}