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
    public class PregledController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Pregled
        public IQueryable<Pregled> GetPregleds()
        {
            return db.Pregleds;
        }


        //// GET: api/Pregled/5
        [ResponseType(typeof(Pregled))]
        public IHttpActionResult GetPregled(int id)
        {
            Pregled pregled = db.Pregleds.Find(id);
            if (pregled == null)
            {
                return NotFound();
            }

            return Ok(pregled);
        }

        [HttpGet]
        [Route("api/Pregled/Detalji/{pacijentId}/{zubId}")]
        public List<usp_Zubi_Result> Detalji(int pacijentId,int zubId)
        {
            return db.usp_Zubi(pacijentId, zubId).ToList();
        }

        // PUT: api/Pregled/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPregled(int id, Pregled pregled)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pregled.Id)
            {
                return BadRequest();
            }

            db.Entry(pregled).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PregledExists(id))
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
        [HttpGet]
        [Route("api/Pregled/GetPregledByPacijent/{pacijentId}")]
        public PregledVM GetPregledByPacijent(int pacijentId)
        {

            PregledVM model = new PregledVM();
            model.PregledLista = db.Pregleds.Where(x => x.PacijentId == pacijentId).Select(x => new PregledVM.PregledInfo
            {
                Id = x.Id,
                Datum=x.DatumPregleda,
                Vrijeme=x.VrijemePregleda

            }).ToList();
            return model;
        }
      

        // POST: api/Pregled
        [ResponseType(typeof(Pregled))]
        public IHttpActionResult PostPregled(Pregled pregled)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pregleds.Add(pregled);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pregled.Id }, pregled);
        }

        // DELETE: api/Pregled/5
        [ResponseType(typeof(Pregled))]
        public IHttpActionResult DeletePregled(int id)
        {
            Pregled pregled = db.Pregleds.Find(id);
            if (pregled == null)
            {
                return NotFound();
            }

            db.Pregleds.Remove(pregled);
            db.SaveChanges();

            return Ok(pregled);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PregledExists(int id)
        {
            return db.Pregleds.Count(e => e.Id == id) > 0;
        }
    }
}