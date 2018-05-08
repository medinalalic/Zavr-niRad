using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class ZakaziTerminVM
    {
        public class TerminInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }

            public string Razlog { get; set; }

            public DateTime Datum { get; set; }
            public DateTime Vrijeme { get; set; }

            public bool Odobren { get; set; }
            public int PacijentId { get; set; }


        }

        public List<TerminInfo> termin;
      
    }
}