using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class DijagnozaDropVM
    {
        public class DijagnozaInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public List<DijagnozaInfo> dijagnoze { get; set; }
    }
}