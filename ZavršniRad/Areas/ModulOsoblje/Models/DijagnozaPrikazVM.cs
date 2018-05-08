using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class DijagnozaPrikazVM
    {
        public IPagedList<Dijagnoza> DijagnozaList { get; set; }

        public string search { get; set; }
    }
}