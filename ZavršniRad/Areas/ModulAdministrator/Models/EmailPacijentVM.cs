using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class EmailPacijentVM
    {
        public List<EmailPacijentVM> ListaPacijenata { get; set; }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string Email { get; set; }
    }
}