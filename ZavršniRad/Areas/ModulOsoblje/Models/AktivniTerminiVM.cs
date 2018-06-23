using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavršniRad.Areas.ModulStomatolog.Models
{
    public class AktivniTerminiVM
    {
        public int Id { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        public string Pon { get; set; }
        public string Uto { get; set; }

        public string Sri { get; set; }
        public string Cet { get; set; }
        public string Pet { get; set; }


        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Da { get; set; }

        public List<AktivniTermini> Termin { get; set; }
        public List<Ponedjeljak> _Ponedjeljak { get; set; }
        public List<Utorak> _Utorak { get; set; }
        public List<Srijeda> _Srijeda { get; set; }
        public List<Cetvrtak> _Cetvrtak { get; set; }
        public List<Petak> _Petak { get; set; }

        public class AktivniTermini
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
        }
        public class Ponedjeljak
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum{ get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }
        }
        public class Utorak
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum { get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }


        }
        public class Srijeda
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum { get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }


        }
        public class Cetvrtak
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum { get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }

        }
        public class Petak
        {
            public int Id { get; set; }
            public DateTime Vrijeme { get; set; }
            public string Pacijent { get; set; }
            public int PacijentId { get; set; }

            public bool Obavljen { get; set; }
            public DateTime Datum { get; set; }
            public string Napomena { get; set; }
            public bool Odobren { get; set; }


        }
       
    }
}