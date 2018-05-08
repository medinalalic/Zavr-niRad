using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class UslugaVM
    {
        public class UslugaInfo
        {  public int Id { get; set; }
        public string Vrsta { get; set; }
        public byte[] Slika { get; set; }

        }
        public List<UslugaInfo> UslugaLista { get; set; }

    }
}