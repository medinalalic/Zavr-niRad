
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavršniRad.Models;

namespace ZavršniRad.Helper
{
    public class Autorizacija : FilterAttribute, IAuthorizationFilter
    {
        private readonly KorisnickeUloge[] _dozvoljeneUloge;

        public Autorizacija(params KorisnickeUloge[] dozvoljeneUloge)
        {
            _dozvoljeneUloge = dozvoljeneUloge;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);
            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login"); return;
            }


            //provjera
            foreach (KorisnickeUloge x in _dozvoljeneUloge)
            {

                if (x == KorisnickeUloge.Pacijent && k.Pacijent != null)
                    return;
                if (x == KorisnickeUloge.Osoblje && k.Osoblje != null && k.IsAdmin == true)
                    return;
                if (x == KorisnickeUloge.Osoblje && k.Osoblje != null)
                    return;
                if (x == KorisnickeUloge.Stomatolog && k.Stomatolog != null)
                    return;
            }


        }







    }
    public enum KorisnickeUloge
    {
        Pacijent,
        Osoblje,
        Stomatolog,
        Administrator
    }
}