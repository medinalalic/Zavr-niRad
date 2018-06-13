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
    public class LijekController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Lijek
        public IQueryable<Lijek> GetLijeks()
        {
            return db.Lijeks;
        }

        // GET: api/Lijek/5
        [ResponseType(typeof(Lijek))]
        public IHttpActionResult GetLijek(int id)
        {
            Lijek lijek = db.Lijeks.Find(id);
            if (lijek == null)
            {
                return NotFound();
            }

            return Ok(lijek);
        }
        [HttpGet]
        [Route("api/Lijek/GetLijekDrop")]
        public LijekVM GetLijekDrop()
        {

            LijekVM model = new LijekVM();
            model.lijekovi = db.Lijeks.Select(x => new LijekVM.LijekInfo
            {
                Id = x.Id,
                Naziv = x.Naziv,


            }).ToList();
            return model;

        }
        // PUT: api/Lijek/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLijek(int id, Lijek lijek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lijek.Id)
            {
                return BadRequest();
            }

            db.Entry(lijek).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LijekExists(id))
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

        // POST: api/Lijek
        [ResponseType(typeof(Lijek))]
        public IHttpActionResult PostLijek(Lijek lijek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lijeks.Add(lijek);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lijek.Id }, lijek);
        }

        // DELETE: api/Lijek/5
        [ResponseType(typeof(Lijek))]
        public IHttpActionResult DeleteLijek(int id)
        {
            Lijek lijek = db.Lijeks.Find(id);
            if (lijek == null)
            {
                return NotFound();
            }

            db.Lijeks.Remove(lijek);
            db.SaveChanges();

            return Ok(lijek);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LijekExists(int id)
        {
            return db.Lijeks.Count(e => e.Id == id) > 0;
        }
    }
}