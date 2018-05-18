using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class HistorijaVM
    {
        public class HistorijaInfo
        {
            public int Id { get; set; }
            public DateTime DatumPregleda { get; set; }
            public DateTime VrijemePregleda { get; set; }
            public bool Uneseno { get; set; }

            public int PacijentId { get; set; }
        }

        public List<HistorijaInfo> history;
        public bool Uneseno { get; set; }
        public int Id { get; set; }



    }
}