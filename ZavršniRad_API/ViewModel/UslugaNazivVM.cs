using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class UslugaNazivVM
    {
        public class UslugaInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public List<UslugaInfo> usluge { get; set; }
    }
}