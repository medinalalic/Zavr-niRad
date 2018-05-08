using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulAdministrator
{
    public class ModulAdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulAdministrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulAdministrator_default",
                "ModulAdministrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}