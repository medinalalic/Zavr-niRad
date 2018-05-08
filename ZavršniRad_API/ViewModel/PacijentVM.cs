using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class PacijentVM
    {
        public int Id { get; set; }
        public string JMBG { get; set; }
        public DateTime AddedOn { get; set; }
    }
}