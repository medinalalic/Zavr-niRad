using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class EmailSendVM
    {
        [Display(Name = ("*Sadržaj email-a"))]
        [Required(ErrorMessage = "Sadržaj e-maila je obavezno polje")]
        public string Body { get; set; }

        [Display(Name = ("*Naziv email-a"))]
        [Required(ErrorMessage = "Naziv e-maila je obavezno polje")]
        public string Subject { get; set; }
        public string From { get; set; }
        [Display(Name = ("*Primaoc"))]
        [Required(ErrorMessage = "Primaoc e-maila je obavezno polje")]
        public string To { get; set; }
        public bool Successed { get; set; }

        public int Id { get; set; }
    }
}