using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class PregledPostVM
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Vrijeme { get; set; }
        public int PacijentId { get; set; }
        public int StomatologId { get; set; }
        public int TerminId { get; set; }
        public bool IsObavljen { get; set; }

        public int UslugaId { get; set; }
        public int ZubId { get; set; }
        public int LijekId { get; set; }
        public int DijagnozaId { get; set; }

        public string  Cijena { get; set; }



    }
}