using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulOsoblje.Models
{
    public class MaterijalPrikazVM
    {
        [DataType(DataType.Text)]
        public string search { get; set; }
        public int MaterijalId { get; set; }
        public int OsobljeId { get; set; }
        public List<SelectListItem> ListaMaterijala { get; set; }

        public IPagedList<PotroseniMaterijal> ListaPotrošenih { get; set; }
    }
}