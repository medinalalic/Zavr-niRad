using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class NoviPregledVM
    {
        public int Id { get; set; }

     [Required]
        [DataType(DataType.DateTime)]
        public DateTime DatumPregleda { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime VrijemePregleda { get; set; }

        public int StomatologId { get; set; }
        public int PacijentId { get; set; }
        public int TerminId { get; set; }
        public bool IsObavljen { get; set; }
       

        public List<SelectListItem> _Zub { get; set; }
        [Required]
        public int? zubID { get; set; }
      

        public List<SelectListItem> _Dijagnoza { get; set; }
        [Required]

        public int? dijagnozaID { get; set; }
       

        public List<SelectListItem> _Lijek { get; set; }
        [Required]

        public int? lijekID { get; set; }
       

        public List<SelectListItem> _Usluga { get; set; }
        [Required]

        public int? uslugaID { get; set; }


        [Required(ErrorMessage ="Cijena je obavezno polje")]
        public decimal Cijena { get; set; }
        [Required]
        [Range(1.0, 5.0)]
        public int Intenzitet { get; set; }
        [Required]
        [Range(1.0, 5.0)]
        public int Vrsta { get; set; }
        [Required(ErrorMessage ="Količina je obavezno polje")]
        [Range(1.0,5.0)]


        public int Kolicina { get; set; }
        [Required(ErrorMessage ="Napomena je obavezno polje")]


        public string Napomena { get; set; }

    }
}