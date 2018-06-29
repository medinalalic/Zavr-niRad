using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.ViewModel
{
    public class KorisniciVM
    {
       
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string Email { get; set; }
        public string Mobitel { get; set; }
        public string Adresa { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool IsAdmin { get; set; }
        public bool Aktivan { get; set; }
    }
}