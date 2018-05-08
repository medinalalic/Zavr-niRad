using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class PregledVM
    {
        public class PregledInfo
        {  public int Id { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Vrijeme { get; set; }

        }
        public List<PregledInfo> PregledLista { get; set; }


    }
}