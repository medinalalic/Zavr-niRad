using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class UrediProfilVM
    {
        [Display(Name = "Korisničko ime")]
        //[Required(ErrorMessage ="Korisničko ime je obavezno polje.")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [StringLength(50, ErrorMessage = " {0} mora imati {2} karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Potvrda lozinke je obavezno")]
        [StringLength(50, ErrorMessage = "{0} mora imati {2} karaktera.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrda lozinke")]
        [System.ComponentModel.DataAnnotations.Compare("Lozinka", ErrorMessage = "Lozinka i potvrda lozinke nisu isti.")]
        public string PotvrdaLozinke { get; set; }


    }
}