using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class PrikaziUslugeVM
    {
        public class UslugaInfo
        {
            public int Id { get; set; }
            public byte[] Slika { get; set; }
            public string Vrsta { get; set; }
        }
        public List<UslugaInfo> usluge;
       // public int Id { get; set; }
      //  public byte[] Slika { get; set; }
      //  public string Vrsta { get; set; }
    }
}