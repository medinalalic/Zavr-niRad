using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class DetaljiVM
    {
       
        public class DetaljiInfo
        {
            public int Id { get; set; }

            public DateTime DatumPregleda { get; set; }
            public string Zub { get; set; }
            public string Dijagnoza { get; set; }
            public string Usluga { get; set; }
            public string Terapija { get; set; }
        }
        public List<DetaljiInfo> detalji;
    }
}