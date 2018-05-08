using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc.Filters;
using ZavršniRad.Models;

namespace ZavršniRad.Helper
{
    public class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContextBase context, bool zapamtiPassword)
        {
            context.Session.Add(LogiraniKorisnik, korisnik);
            if (zapamtiPassword)
            {
                HttpCookie cookie = new HttpCookie("_mvc_session", korisnik != null ? korisnik.Id.ToString() : "");

                context.Response.Cookies.Add(cookie);
            }
        }

        public static Korisnik GetLogiraniKorisnik(HttpContextBase context)
        {
            Korisnik korisnik = (Korisnik)context.Session[LogiraniKorisnik];

            if (korisnik != null)
                return korisnik;

            HttpCookie cookie = context.Request.Cookies.Get("_mvc_session");

            if (cookie == null)
                return null;

            long userId;
            try
            {
                userId = Int64.Parse(cookie.Value);
            }
            catch
            {
                return null;
            }


            using (Stomatoloska_MLEntities1 db = new Stomatoloska_MLEntities1())
            {
                Korisnik k = db.Korisniks
                   .Include(x => x.Pacijent)
                   .Include(x => x.Osoblje)
                   .Include(x => x.Stomatolog)
                    .SingleOrDefault(x => x.Id == userId);

                PokreniNovuSesiju(k, context, true);
                return k;
            }
        }






    }
}
