using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class DijagnozaDodajVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [Display(Name = "Naziv")]
        public string Naziv { get; set; }
    }
}