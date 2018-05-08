using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class SendEmail
    {
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int pacijentId { get; set; }
    }
}