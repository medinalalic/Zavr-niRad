using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class RacunPrikaz
    {
        public class RacunInfo
        {
            public int Id { get; set; }

            public bool Uplaćeno { get; set; }

            public decimal Cijena { get; set; }
            public DateTime Datum { get; set; }
            public string Pacijent { get; set; }
        }
        public List<RacunInfo> racun;
        public string p { get; set; }
        public int? pacijentID { get; set; }
        public List<SelectListItem> Pacijent { get; set; }
    }
}
