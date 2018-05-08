using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class RegistracijaVM
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
        [Required(ErrorMessage = "Korisničko ime je obavezno polje!")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Korisničko ime smije sadržavati samo slova!")]

        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezno polje!")]
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,20})", ErrorMessage = "Lozinka mora sadržavati broj, malo i veliko slovo! Veličina lozinke mora biti u rasponu 8-20 karaktera!")]
        public string Lozinka { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
    }
}