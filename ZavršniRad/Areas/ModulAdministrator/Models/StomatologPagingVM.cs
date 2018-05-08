using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class StomatologPagingVM
    {
        public List<Stomatolog> StomatologList { get; set; }

        public string search { get; set; }
    }
}