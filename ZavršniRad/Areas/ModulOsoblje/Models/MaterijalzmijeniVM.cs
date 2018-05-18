using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class MaterijalzmijeniVM
    {
        public List<SelectListItem> materijal { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Količina je obavezno polje !")]
        [Range(1.0, 10.0, ErrorMessage = "Količinu možete izabrati u rasponu 1-10 !")]
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }
        public int? OsobljeId { get; set; }
   
        public int? MaterijalId { get; set; }
    }
}