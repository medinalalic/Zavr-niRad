using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class PorukaVM
    {
        public int Id { get; set; }
        public int PacijentId { get; set; }
        public int StomatologId { get; set; }
        public string TekstPoruke { get; set; }
        public string Datum { get; set; }
        public bool Procitana { get; set; }
    }
}