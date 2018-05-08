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
        public int? zubID { get; set; }
        public List<SelectListItem> _Dijagnoza { get; set; }
        public int? dijagnozaID { get; set; }
        public List<SelectListItem> _Lijek { get; set; }
        public int? lijekID { get; set; }
        public List<SelectListItem> _Usluga { get; set; }
        public int? uslugaID { get; set; }



        public decimal Cijena { get; set; }
        public int Intenzitet { get; set; }
        public int Vrsta { get; set; }

        public int Kolicina { get; set; }

        public string Napomena { get; set; }

    }
}