﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class CetvrtakVM
    {
      
        public List<Cetvrtak> _Cetvrtak { get; set; }
       


        public class Cetvrtak
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum { get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }

        }
    }
}