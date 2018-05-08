
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class UslugePagingVM
    {
        public IPagedList<Usluga> UslugeList { get; set; }

        public string search { get; set; }
    }
}