using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class SlobodniVM
    {
        public int Id { get; set; }
        public string Razlog { get; set; }
        public bool Odobren { get; set; }

        public DateTime Datum { get; set; }

        public DateTime Vrijeme { get; set; }

        public int PacijentId { get; set; }
        public List<string> satnice { get; set; }
    }
}