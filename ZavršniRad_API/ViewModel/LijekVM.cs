using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class LijekVM
    {
        public class LijekInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public List<LijekInfo> lijekovi { get; set; }
    }
}