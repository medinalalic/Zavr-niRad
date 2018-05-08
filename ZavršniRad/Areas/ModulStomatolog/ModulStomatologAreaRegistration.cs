using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulStomatolog
{
    public class ModulStomatologAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulStomatolog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulStomatolog_default",
                "ModulStomatolog/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}