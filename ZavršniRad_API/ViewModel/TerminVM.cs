using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class TerminVM
    {
        public int Id;
        public string Datum;
        public string Vrijeme;
        public bool Odobren;
        public int PacijentId;
        public string RazlogPosjete;


    }
}