using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class RacunIzmijeni
    {
        public int Id { get; set; }
        public bool Uplaćeno { get; set; }
        [Required(ErrorMessage = "Cijena je obavezno polje")]
        [DataType(DataType.Currency)]
        public decimal Cijena { get; set; }
        [Required(ErrorMessage = "Datum je obavezno polje!")]
        public DateTime Datum { get; set; }
        public int? pacijentID { get; set; }
        public List<SelectListItem> pacijenti { get; set; }
        public int osobljeID { get; set; }

    }
}