using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ZavršniRad.Helper;
using ZavršniRad_API;
using ZavršniRad_API.ViewModel;
using static ZavršniRad_API.ViewModel.PonedjeljakVM;

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
        [HttpPost]
        [ResponseType(typeof(Termin))]
        public IHttpActionResult PostTermin(TerminVM termin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var date = DateTime.ParseExact(termin.Vrijeme,
              "yyyy-MM-dd'T'HH:mm:ss",
              CultureInfo.InvariantCulture);
            Termin t = new Termin();
            t.Datum = date;
            t.Id = termin.Id;
            t.Odobren = termin.Odobren;
            t.PacijentId = termin.PacijentId;
            t.RazlogPosjete = termin.RazlogPosjete;
            t.Vrijeme = date;

            db.Termins.Add(t);
            db.SaveChanges();
           
                return CreatedAtRoute("DefaultApi", new { id = termin.Id }, termin); 
            
            
        }
        [HttpGet]
        [Route("api/Termin/GetZauzete")]
        public List<usp_ZauzetiTermini_Result> GetZauzete()
        {
            return db.usp_ZauzetiTermini().ToList();
        }

        [HttpGet]
        [Route("api/Termin/GetProsle")]
        public List<usp_ProsliTermini_Result> GetProsle()
        {
            return db.usp_ProsliTermini().ToList();
        }

        [HttpGet]
        [Route("api/Termin/GetDanas")]
        public List<usp_DanasnjiTermini_Result> GetDanas()
        {
            return db.usp_DanasnjiTermini().ToList();
        }

        [HttpGet]
        [Route(@"api/Termin/TerminRazlogDatum/{datum}/{razlog}")]
        public SlobodniVM TerminRazlogDatum(string datum, string razlog)
        {

            SlobodniVM model = new SlobodniVM();
            List<string> SlobodnihTermina = new List<string>();

            List<string> satnice = new List<string> { "09:00", "09:30", "10:00","10:30",
            "11:00","11:30","12:00","12:30","13:00","15:00","15:30","16:00","16:30"};

            var terminis = satnice
              .Select(i =>
              {
                  TimeSpan result;
                  if (TimeSpan.TryParse(i, out result))
                      return new Nullable<TimeSpan>(result);
                  return null;
              })
              .Where(x => x.HasValue)
              .ToList();

            var date = DateTime.ParseExact(datum,
               "yyyy-MM-dd'T'HH:mm:ss",
               CultureInfo.InvariantCulture);
            DateTime d = date;
            DateTime vrijeme = date;
            if (terminis != null)
            {
                foreach (var x in terminis)
                {
                    
                    d = date.Date + x.Value;
                    vrijeme = date.Date + x.Value;
                    if (slobodan(d))
                    {
                        SlobodnihTermina.Add(x.ToString());
                    }
                }

            }
            model.Razlog = razlog;
            model.satnice = SlobodnihTermina;
            model.Datum = d;
            model.Vrijeme = vrijeme;
            return model;

        }

        public bool slobodan(DateTime datum)
        {
           int b = db.Termins.Where(a => datum.Day == a.Vrijeme.Day && datum.Month == a.Vrijeme.Month
                      && datum.Year == a.Vrijeme.Year && datum.Hour == a.Vrijeme.Hour
                      && datum.Minute == a.Vrijeme.Minute).Count();
            
            if (b == 0)
                return true;
            else
                return false;
        }



        [HttpGet]
        [Route("api/Termin/GetNaredni")]
        public List<usp_NaredniTermini_Result> GetNaredni()
        {
            return db.usp_NaredniTermini().ToList();
        }
        [HttpGet]
        [ResponseType(typeof(bool))]

        [Route(@"api/Termin/IsSlobodanTermin/{datum}")]
        public bool IsSlobodanTermin(string datum)
        {
            var date = DateTime.ParseExact(datum,
                "yyyy-MM-dd'T'HH:mm:ss",
                CultureInfo.InvariantCulture);
            Console.WriteLine(date);

            var rezultat = true;
            var lista = db.Termins.ToList();
            foreach (var x in lista)
            {
                if (date == x.Vrijeme)
                {
                    rezultat = false;
                    return rezultat;
                }
                else
                {
                    rezultat = true;
                }

            }
            return rezultat;
        }

        [HttpGet]
        [ResponseType(typeof(bool))]

        [Route(@"api/Termin/Odobren/{pacijentId}")]
        public bool Odobren(int pacijentId)
        {
           Termin t=  db.Termins.Where(x => x.PacijentId == pacijentId && x.Odobren == true && x.Datum.Year.Equals(DateTime.Now.Year) && x.Datum.Month.Equals(DateTime.Now.Month) && x.Datum.Day.Equals(DateTime.Now.Day)
               ).FirstOrDefault();
            if (t != null)
                return true;
            else return false;
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
        public static DateTime PosljedniDanSedmice(DateTime date)
        {
            
            DateTime ldowDate = PrviDanSedmice(date).AddDays(6);
            
            return ldowDate;
        }
        [HttpGet]
        [Route("api/Termin/DatumiSedmiceTrenutne")]
        public TrenutnaDatumiVM DatumiSedmiceTrenutne()
        {
            TrenutnaDatumiVM model = new TrenutnaDatumiVM();

            DateTime date = DateTime.Now;
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset+1);
            model.Pon = fdowDate;
            model.Uto = model.Pon.AddDays(1);
            model.Sri = model.Uto.AddDays(1);
            model.Cet = model.Sri.AddDays(1);
            model.Pet = model.Cet.AddDays(1);
            model.Sub = model.Pet.AddDays(1);
            model.Ned = model.Sub.AddDays(1);

            return model;
        }

        [HttpGet]
        [Route("api/Termin/DatumiNaredneSedmice")]
        public TrenutnaDatumiVM DatumiNaredneSedmice()
        {
            TrenutnaDatumiVM model = new TrenutnaDatumiVM();
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);

            model.Pon = startDate;
            model.Uto = model.Pon.AddDays(1);
            model.Sri = model.Uto.AddDays(1);
            model.Cet = model.Sri.AddDays(1);
            model.Pet = model.Cet.AddDays(1);
            model.Sub = model.Pet.AddDays(1);
            model.Ned = model.Sub.AddDays(1);

            return model;
        }

        [HttpGet]
        [Route("api/Termin/DatumiProsleSedmice")]
        public TrenutnaDatumiVM DatumiProsleSedmice()
        {
            TrenutnaDatumiVM model = new TrenutnaDatumiVM();
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date).AddDays(1);

            model.Pon = startDate;
            model.Uto = model.Pon.AddDays(1);
            model.Sri = model.Uto.AddDays(1);
            model.Cet = model.Sri.AddDays(1);
            model.Pet = model.Cet.AddDays(1);
            model.Sub = model.Pet.AddDays(1);
            model.Ned = model.Sub.AddDays(1);

            return model;
        }

        public static  DateTime PrviDanSedmice(DateTime date)
        {
            
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
           
            return fdowDate;
        }
        //trenutna sedmica
        [HttpGet]
        [Route("api/Termin/PonedjeljakTermini")]
        [ResponseType(typeof(PonedjeljakVM))]

        public PonedjeljakVM PonedjeljakTermini()
        {

            var model = new PonedjeljakVM();

            DateTime startDate = PrviDanSedmice(DateTime.Now);

            DateTime pon = startDate.AddDays(1);
            model._Ponedjeljak = db.Termins
                .Where(x => x.Datum.Year.Equals(pon.Year) && x.Datum.Month.Equals(pon.Month) && x.Datum.Day.Equals(pon.Day))
                 .Select(
                a => new PonedjeljakVM.Ponedjeljak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }

        [HttpGet]
        [ResponseType(typeof(UtorakVM))]
        [Route("api/Termin/UtorakTermini")]
        public UtorakVM UtorakTermini()
        {

            var model = new UtorakVM();

            var startDate = PrviDanSedmice(DateTime.Now);
            var endDate = PosljedniDanSedmice(DateTime.Now);


            var utorak = startDate.AddDays(2);
            model._Utorak = db.Termins
                .Where(x => x.Datum.Year.Equals(utorak.Year) && x.Datum.Month.Equals(utorak.Month) && x.Datum.Day.Equals(utorak.Day))
                .Select(
                a => new UtorakVM.Utorak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [ResponseType(typeof(SrijedaVM))]
        [Route("api/Termin/SrijedaTermini")]
        public SrijedaVM SrijedaTermini()
        {

            var model = new SrijedaVM();

            var startDate = PrviDanSedmice(DateTime.Now);
            var endDate = PosljedniDanSedmice(DateTime.Now);


            var dat = startDate;
            var utorak = dat.AddDays(2);
            var srijeda = utorak.AddDays(1);

            var datEnd = endDate;
            model._Srijeda = db.Termins
                .Where(x => x.Datum.Year.Equals(srijeda.Year) && x.Datum.Month.Equals(srijeda.Month) && x.Datum.Day.Equals(srijeda.Day))
                .Select(
                a => new SrijedaVM.Srijeda
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [ResponseType(typeof(CetvrtakVM))]
        [Route("api/Termin/CetvrtakTermini")]
        public CetvrtakVM CetvrtakTermini()
        {

            var model = new CetvrtakVM();

            var startDate = PrviDanSedmice(DateTime.Now);
            var endDate = PosljedniDanSedmice(DateTime.Now);


            var dat = startDate;
            var utorak = dat.AddDays(2);
            var srijeda = utorak.AddDays(1);
            var cet = srijeda.AddDays(1);
            var datEnd = endDate;
            model._Cetvrtak = db.Termins
                .Where(x => x.Datum.Year.Equals(cet.Year) && x.Datum.Month.Equals(cet.Month) && x.Datum.Day.Equals(cet.Day))
                .Select(
                a => new CetvrtakVM.Cetvrtak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [ResponseType(typeof(PetakVM))]
        [Route("api/Termin/PetakTermini")]
        public PetakVM PetakTermini()
        {

            var model = new PetakVM();

            var startDate = PrviDanSedmice(DateTime.Now);
            var endDate = PosljedniDanSedmice(DateTime.Now);


            var dat = startDate;
            var utorak = dat.AddDays(2);
            var srijeda = utorak.AddDays(1);
            var cet = srijeda.AddDays(1);
            var pet = cet.AddDays(1);
            var datEnd = endDate;
            model._Petak = db.Termins
                .Where(x => x.Datum.Year.Equals(pet.Year) && x.Datum.Month.Equals(pet.Month) && x.Datum.Day.Equals(pet.Day))
                .Select(
                a => new PetakVM.Petak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        //naredna sedmica
        public static DateTime PrviDanNaredneSedmice(DateTime date)
        {
            var nowdate = date.AddDays(1);
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }
        [HttpGet]
        [Route("api/Termin/SlijedecaSedmicaPonedjeljak")]
        [ResponseType(typeof(PonedjeljakVM))]

        public PonedjeljakVM SlijedecaSedmicaPonedjeljak()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);

            //var dae = PosljedniDanSedmice(DateTime.Now);
            // var dates = dae.Date.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);
            //var date = PrviDanNaredneSedmice(dae);
            //   var startDate = date.AddDays(1);


            var model = new PonedjeljakVM();
            model._Ponedjeljak = db.Termins
                .Where(x => x.Datum.Year.Equals(startDate.Year) && x.Datum.Month.Equals(startDate.Month) && x.Datum.Day.Equals(startDate.Day))
                .Select(
                a => new PonedjeljakVM.Ponedjeljak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/SlijedecaSedmicaUtorak")]
        [ResponseType(typeof(UtorakVM))]

        public UtorakVM SlijedecaSedmicaUtorak()
        {

            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);

            var model = new UtorakVM();
            model._Utorak = db.Termins
                .Where(x => x.Datum.Year.Equals(uto.Year) && x.Datum.Month.Equals(uto.Month) && x.Datum.Day.Equals(uto.Day))
                .Select(
                a => new UtorakVM.Utorak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/SlijedecaSedmicaSrijeda")]
        [ResponseType(typeof(SrijedaVM))]

        public SrijedaVM SlijedecaSedmicaSrijeda()
        {

            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);

            var model = new SrijedaVM();
            model._Srijeda = db.Termins
                .Where(x => x.Datum.Year.Equals(sri.Year) && x.Datum.Month.Equals(sri.Month) && x.Datum.Day.Equals(sri.Day))
                .Select(
                a => new SrijedaVM.Srijeda
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/SlijedecaSedmicaCetvrtak")]
        [ResponseType(typeof(CetvrtakVM))]

        public CetvrtakVM SlijedecaSedmicaCetvrtak()
        {

            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);
            var cet = sri.AddDays(1);

            var model = new CetvrtakVM();
            model._Cetvrtak = db.Termins
                .Where(x => x.Datum.Year.Equals(cet.Year) && x.Datum.Month.Equals(cet.Month) && x.Datum.Day.Equals(cet.Day))
                .Select(
                a => new CetvrtakVM.Cetvrtak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/SlijedecaSedmicaPetak")]
        [ResponseType(typeof(PetakVM))]

        public PetakVM SlijedecaSedmicaPetak()
        {

            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(7).AddHours(0).AddMinutes(0).AddSeconds(0);
            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);
            var cet = sri.AddDays(1);
            var pet = cet.AddDays(1);

            var model = new PetakVM();
            model._Petak = db.Termins
                .Where(x => x.Datum.Year.Equals(pet.Year) && x.Datum.Month.Equals(pet.Month) && x.Datum.Day.Equals(pet.Day))
                .Select(
                a => new PetakVM.Petak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        //prosla sedmica
        [HttpGet]
        [Route("api/Termin/ProslaSedmicaPonedjeljak")]
        [ResponseType(typeof(PonedjeljakVM))]

        public PonedjeljakVM ProslaSedmicaPonedjeljak()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

             var startDate = PrviDanSedmice(date).AddDays(1);


            var model = new PonedjeljakVM();
            model._Ponedjeljak = db.Termins
                .Where(x => x.Datum.Year.Equals(startDate.Year) && x.Datum.Month.Equals(startDate.Month) && x.Datum.Day.Equals(startDate.Day))
                .Select(
                a => new PonedjeljakVM.Ponedjeljak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/ProslaSedmicaUtorak")]
        [ResponseType(typeof(UtorakVM))]

        public UtorakVM ProslaSedmicaUtorak()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);

            var model = new UtorakVM();
            model._Utorak = db.Termins
                .Where(x => x.Datum.Year.Equals(uto.Year) && x.Datum.Month.Equals(uto.Month) && x.Datum.Day.Equals(uto.Day))
                .Select(
                a => new UtorakVM.Utorak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/ProslaSedmicaSrijeda")]
        [ResponseType(typeof(SrijedaVM))]

        public SrijedaVM ProslaSedmicaSrijeda()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);
            var model = new SrijedaVM();
            model._Srijeda = db.Termins
                .Where(x => x.Datum.Year.Equals(sri.Year) && x.Datum.Month.Equals(sri.Month) && x.Datum.Day.Equals(sri.Day))
                .Select(
                a => new SrijedaVM.Srijeda
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/ProslaSedmicaCetvrtak")]
        [ResponseType(typeof(CetvrtakVM))]

        public CetvrtakVM ProslaSedmicaCetvrtak()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);
            var cet = sri.AddDays(1);
            var model = new CetvrtakVM();
            model._Cetvrtak = db.Termins
                .Where(x => x.Datum.Year.Equals(cet.Year) && x.Datum.Month.Equals(cet.Month) && x.Datum.Day.Equals(cet.Day))
                .Select(
                a => new CetvrtakVM.Cetvrtak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        [HttpGet]
        [Route("api/Termin/ProslaSedmicaPetak")]
        [ResponseType(typeof(PetakVM))]

        public PetakVM ProslaSedmicaPetak()
        {
            var da = PrviDanSedmice(DateTime.Now);
            var dae = Convert.ToDateTime(da);
            var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

            var startDate = PrviDanSedmice(date).AddDays(1);
            var uto = startDate.AddDays(1);
            var sri = uto.AddDays(1);
            var cet = sri.AddDays(1);
            var pet = cet.AddDays(1);
            var model = new PetakVM();
            model._Petak = db.Termins
                .Where(x => x.Datum.Year.Equals(pet.Year) && x.Datum.Month.Equals(pet.Month) && x.Datum.Day.Equals(pet.Day))
                .Select(
                a => new PetakVM.Petak
                {
                    Id = a.Id,
                    Vrijeme = a.Vrijeme,
                    Pacijent = db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Ime + " " + db.Pacijents.FirstOrDefault(s => s.Id == a.PacijentId).Korisnik.Prezime
                    ,
                    Odobren = a.Odobren,
                    PacijentId = a.PacijentId,
                    Datum = a.Datum,
                    Napomena = a.RazlogPosjete
                }).ToList();

            return model;
        }
        ////public ActionResult ProslaSedmicaTermini(DateTime da)
        //{

        //    var model = new RezervacijeVM();
        //    var dae = Convert.ToDateTime(da);
        //    var date = dae.Date.AddDays(-7).AddHours(0).AddMinutes(0).AddSeconds(0);

        //    var startDate = PrviDanSedmice(date);

        //    var endDate = PosljednjiDanSedmice(date);

        //    var startFormat = startDate.ToString("yyy-MM-dd");
        //    var dat = Convert.ToDateTime(startFormat);

        //    var EndFormat = endDate.ToString("yyy-MM-dd");
        //    var datEnd = Convert.ToDateTime(EndFormat);

        //    var utora = dat.AddDays(1);
        //    var srijeda = utora.AddDays(1);
        //    var cetr = srijeda.AddDays(1);
        //    var petak = cetr.AddDays(1);
        //    var subota = petak.AddDays(1);

        //    model.da = startDate.ToString();
        //    model.pocetak = startDate;
        //    model.kraj = endDate;
        //    model._Ponedeljak = ctx.Rezervacije
        //        .Where(x => x.DatumPregleda >= dat && x.DatumPregleda <= dat && x.satnicaRasporedId != null)
        //        .Select(
        //        a => new RezervacijeVM.Ponedeljak
        //        {
        //            Id = a.Id,
        //            Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //            Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //            ,
        //            DatumPregleda = a.DatumPregleda,
        //            Napomena = a.Napomena,
        //            IsDeleted = a.IsDeleted
        //        }).ToList();

        //    model._Utorak = ctx.Rezervacije
        //       .Where(x => x.DatumPregleda >= utora && x.DatumPregleda <= utora && x.satnicaRasporedId != null)
        //       .Select(
        //       a => new RezervacijeVM.Utorak
        //       {
        //           Id = a.Id,
        //           Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //           Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //           ,
        //           Napomena = a.Napomena,
        //           IsDeleted = a.IsDeleted
        //       }).ToList();

        //    model._Srijeda = ctx.Rezervacije
        //      .Where(x => x.DatumPregleda >= srijeda && x.DatumPregleda <= srijeda && x.satnicaRasporedId != null)
        //      .Select(
        //      a => new RezervacijeVM.Srijeda
        //      {
        //          Id = a.Id,
        //          Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //          Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //            ,
        //          Napomena = a.Napomena,
        //          IsDeleted = a.IsDeleted
        //      }).ToList();

        //    model._Cetvrtak = ctx.Rezervacije
        //      .Where(x => x.DatumPregleda >= cetr && x.DatumPregleda <= cetr && x.satnicaRasporedId != null)
        //      .Select(
        //      a => new RezervacijeVM.Cetvrtak
        //      {
        //          Id = a.Id,
        //          Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //          Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //            ,
        //          Napomena = a.Napomena,
        //          IsDeleted = a.IsDeleted
        //      }).ToList();


        //    model._Petak = ctx.Rezervacije
        //      .Where(x => x.DatumPregleda >= petak && x.DatumPregleda <= petak && x.satnicaRasporedId != null)
        //      .Select(
        //      a => new RezervacijeVM.Petak
        //      {
        //          Id = a.Id,
        //          Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //          Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //            ,
        //          Napomena = a.Napomena,
        //          IsDeleted = a.IsDeleted
        //      }).ToList();


        //    model._Subota = ctx.Rezervacije
        //      .Where(x => x.DatumPregleda >= subota && x.DatumPregleda <= subota && x.satnicaRasporedId != null)
        //      .Select(
        //      a => new RezervacijeVM.Subota
        //      {
        //          Id = a.Id,
        //          Satnica = ctx.SatnicaRaspored.FirstOrDefault(d => d.Id == a.satnicaRasporedId).Vrijeme,
        //          Pacijent = ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Ime + " " + ctx.Pacijenti.FirstOrDefault(s => s.Id == a.LoginPodaciId).Prezime
        //            ,
        //          Napomena = a.Napomena,
        //          IsDeleted = a.IsDeleted
        //      }).ToList();

        //    return View("Rezervacije", model);
        //}

        public static DateTime PosljedniDanNaredneSedmice(DateTime date)
        {
            DateTime ldowDate = PrviDanNaredneSedmice(date).AddDays(6);
            return ldowDate;
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