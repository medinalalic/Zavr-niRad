using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using ZavršniRad_API;
using ZavršniRad_API.ViewModel;

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
        [HttpGet]
        [ResponseType(typeof(Pacijent))]
        [Route("api/Pacijent/GetSlika/{id}")]
        public Pacijent GetSlika(int id)
        {
            Pacijent pacijent = db.Pacijents.Find(id);
      
            return pacijent;
        }
        
        [HttpPost]
        [Route("api/Pacijent/IzmjenaPodatka")]
        public IHttpActionResult IzmjenaPodatka(PacijentVM k)
        {
            Pacijent korisnik = new Pacijent();
            korisnik.Id = k.Id;

            //  byte[] bytes = Encoding.UTF8.GetBytes(k.Slika);
            // korisnik.Slika = bytes;
          
        var s = Base64ToImage(k.Slika);
        korisnik.Slika= imageToByteArray(s);
        
         

        db.Entry(korisnik).State = EntityState.Modified;
            db.SaveChanges();


            return Ok();
        }
        public Image Base64ToImage(string base64String)
        {
            
            byte[] imageBytes = Convert.FromBase64String(base64String);

            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            
                Image image = Image.FromStream(ms, true);
                return image;
            
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
           
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public Image stringToImage(string inputString)
        {
            byte[] imageBytes = Encoding.Unicode.GetBytes(inputString);

            // Don't need to use the constructor that takes the starting offset and length
            // as we're using the whole byte array.
            MemoryStream ms = new MemoryStream(imageBytes);

            Image image = Image.FromStream(ms, true, true);

            return image;
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