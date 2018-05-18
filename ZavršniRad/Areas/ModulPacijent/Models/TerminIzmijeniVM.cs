using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class TerminIzmijeniVM
    {

       
        public int Id { get; set; }
        [Required(ErrorMessage = "Razlog je obavezno polje!")]
        public string Razlog { get; set; }
        public bool Odobren { get; set; }
        [Required(ErrorMessage = "Datum je obavezno polje!")]
        
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Vrijeme je obavezno polje!")]
        //[DataType(DataType.Time, ErrorMessage = "Format vremena nije tačno definisan!")]
        public DateTime Vrijeme { get; set; }
        public int PacijentId { get; set; }


    }
}