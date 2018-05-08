using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulOsoblje
{
    public class ModulOsobljeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulOsoblje";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulOsoblje_default",
                "ModulOsoblje/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}