using System.Web.Mvc;

namespace ZavršniRad.Areas.ModulPacijent
{
    public class ModulPacijentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulPacijent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulPacijent_default",
                "ModulPacijent/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}