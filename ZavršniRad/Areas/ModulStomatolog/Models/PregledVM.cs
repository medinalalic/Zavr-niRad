using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class PregledVM
    {
        public int Id { get; set; }

        public DateTime DatumPregleda { get; set; }
        public DateTime VrijemePregleda { get; set; }
 public string NazivUsluge { get; set; }
        public decimal CijenaUsluge { get; set; }
        public string NazivZuba { get; set; }
        public string Napomena { get; set; }
        public string Lijek { get; set; }
        public int Kolicina { get; set; }

        public string Dijagnoza { get; set; }
        


    }
}