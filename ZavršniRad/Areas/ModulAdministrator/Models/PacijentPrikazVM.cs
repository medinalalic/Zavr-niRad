using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class PacijentPrikazVM
    {
        public class PacijentInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string Mobitel { get; set; }
            public string Adresa { get; set; }
            public string JMBG { get; set; }
            public string KorisnickoIme { get; set; }
            public string Lozinka { get; set; }
            public bool IsAdmin { get; set; }
            public bool Aktivan { get; set; }


        }
        public List<PacijentInfo> pacijent;
        
    }
}