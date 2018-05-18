using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Models;
using System.ComponentModel.DataAnnotations;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class OsobljeIzmijeniVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Ime je obavezno polje!")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Ime smije sadržavati samo slova!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje!")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Prezime smije sadržavati samo slova!")]

        public string Prezime { get; set; }
        [Required(ErrorMessage = "Email je obavezno polje!")]
        [EmailAddress(ErrorMessage = "Morate unijeti validan email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobitel je obavezno polje!")]
        [RegularExpression("([06]{2}[0,1,2,3,4][0-9]{3}[0-9]{3,4})", ErrorMessage = "Broj mobitela nije validan!")]
        public string Mobitel { get; set; }
        [Required(ErrorMessage = "Adresa je obavezno polje!")]
      
        public string Adresa { get; set; }
        [Required(ErrorMessage = "JMBG je obavezno polje!")]
        [RegularExpression("^[0-9]{13}", ErrorMessage = "JMBG može sadržavati samo brojeve!")]
        public string JMBG { get; set; }
        [Required(ErrorMessage = "Titula je obavezno polje!")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{2,}", ErrorMessage = "Titula smije sadržavati samo slova!")]

        public string Titula { get; set; }
        [Required(ErrorMessage = "Korisničko ime je obavezno polje!")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Korisničko ime smije sadržavati samo slova!")]

        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezno polje!")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        public bool IsAdmin { get; set; }
        public bool Aktivan { get; set; }


    }
}