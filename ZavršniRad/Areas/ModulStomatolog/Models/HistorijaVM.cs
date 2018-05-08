using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavršniRad.Models;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class HistorijaVM
    {
 //      public class HistorijaInfo
 //       {
 //public int Id { get; set; }
 //           public DateTime DatumPregleda { get; set; }
 //       public DateTime VrijemePregleda { get; set; }

 //       public int PacijentId { get; set; }
 //       }

 //       public List<HistorijaInfo> history;
        public IPagedList<Pregled> PregledList { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public string pacijent { get; set; }

        public List<IzvrsenaUsluga> IzvUsluga { get; set; }

        public class IzvrsenaUsluga
        {
            public int IzvrsenaId { get; set; }
            public decimal Cijena { get; set; }
            public string Naziv { get; set; }
            public string NazivZuba { get; set; }
        }

        public List<UspostavljenaDijagnoza> UspDijagnoza { get; set; }

        public class UspostavljenaDijagnoza
        {
            public int UspId { get; set; }
            public string Naziv { get; set; }
            public string Napomena { get; set; }

        }
        public List<Terapija> Lijek { get; set; }

        public class Terapija
        {
            public int TerapijaId { get; set; }
            public string Lijek { get; set; }
            public int Kolicina { get; set; }
        }


    }
}