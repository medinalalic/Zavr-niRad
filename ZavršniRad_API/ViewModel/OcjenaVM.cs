using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class OcjenaVM
    {
        public int Id { get; set; }
        public DateTime DatumOcjenjivanja { get; set; }
        public int OcjenaInt { get; set; }
        public int UslugaId { get; set; }
        public int PacijentId { get; set; }
    }
}