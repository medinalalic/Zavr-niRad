using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class SlobodniVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Razlog je obavezno polje!")]
        public string Razlog { get; set; }
        public bool Odobren { get; set; }
        [Required(ErrorMessage = "Datum je obavezno polje!")]

        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Vrijeme je obavezno polje!")]

        public DateTime Vrijeme { get; set; }

        public int PacijentId { get; set; }
        public List<string> satnice { get; set; }
    }
}