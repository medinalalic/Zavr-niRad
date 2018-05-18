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
    public class TerminController : ApiController
    {
        private Stomatoloska_MLEntities db = new Stomatoloska_MLEntities();

        // GET: api/Termin
        public IQueryable<Termin> GetTermins()
        {
            return db.Termins;
        }

        // GET: api/Termin/5
        [ResponseType(typeof(Termin))]
        public IHttpActionResult GetTermin(int id)
        {
            Termin termin = db.Termins.Find(id);
            if (termin == null)
            {
                return NotFound();
            }

            return Ok(termin);
        }
        [HttpGet]
        [Route("api/Termin/PretragaTermina/{datum1}/{datum2}")]
        public List<usp_TerminiPretraga_Result> PretragaTermina(DateTime datum1,DateTime datum2)
        {

            return db.usp_TerminiPretraga(datum1,datum2).ToList();
        }
        // PUT: api/Termin/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTermin(int id, Termin termin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != termin.Id)
            {
                return BadRequest();
            }

            db.Entry(termin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminExists(id))
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

        // POST: api/Termin
        [ResponseType(typeof(Termin))]
        public IHttpActionResult PostTermin(Termin termin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rezultat = false;
            var lista = db.Termins.Where(a => a.Datum >= DateTime.Today);
            foreach (var x in lista)
            {
                if (termin.Datum == x.Datum)
                {
                    if (termin.Vrijeme == x.Vrijeme)
                    {
                        rezultat = false;
                    }
                    else
                    {
                        rezultat = true;
                    }
                }
                else
                {
                    rezultat = true;
                }

            }
            if (rezultat == true)
            {
                db.Termins.Add(termin);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = termin.Id }, termin);
            }
            else return BadRequest();
        }


        [HttpGet]
        [Route("api/Termin/IsSlobodanTermin/{datum}/{vrijeme}")]
        public TerminVM IsSlobodanTermin(DateTime datum,DateTime vrijeme)
        {
            var rezultat=false;
           
            TerminVM t = new TerminVM();
            if (rezultat == true)
            {

                t.Slobodan = true;
                return t;

            }
            else
            {
                t.Slobodan = false;
                return t;

            }
 }



        // DELETE: api/Termin/5
        [ResponseType(typeof(Termin))]
        public IHttpActionResult DeleteTermin(int id)
        {
            Termin termin = db.Termins.Find(id);
            if (termin == null)
            {
                return NotFound();
            }

            db.Termins.Remove(termin);
            db.SaveChanges();

            return Ok(termin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerminExists(int id)
        {
            return db.Termins.Count(e => e.Id == id) > 0;
        }
    }
}