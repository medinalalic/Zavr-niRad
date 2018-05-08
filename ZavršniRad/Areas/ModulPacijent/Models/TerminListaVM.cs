using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulPacijent.Models
{
    public class TerminListaVM
    {
        public IPagedList<Termin> TerminList { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
    }
}