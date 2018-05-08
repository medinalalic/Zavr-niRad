using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class OsobljePagingVM
    {
        public List<Osoblje> OsobljeList { get; set; }

        public string search { get; set; }
    }
}