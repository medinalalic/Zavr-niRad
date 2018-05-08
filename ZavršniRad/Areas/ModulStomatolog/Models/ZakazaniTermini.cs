using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class ZakazaniTermini
    {
        public class TerminInfo
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public string Razlog { get; set; }
            public bool Odobren { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }

        }
        public string Pacijent { get; set; }

        public int PacijentId { get; set; }
        public int Id { get; set; }
        public List<TerminInfo> termin;

    }
}