using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class UslugeUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [Display(Name = "Naziv")]
        public string Vrsta { get; set; }
        [Required(ErrorMessage = "Slika je obavezno polje")]
        [Display(Name = "Slika")]
        public byte[] Slika { get; set; }
        public string ImagePath { get; set; }


    }
}