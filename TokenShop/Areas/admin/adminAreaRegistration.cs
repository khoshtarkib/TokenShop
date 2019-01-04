using System.Web.Mvc;

namespace TokenShop.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller="home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TokenShop.Areas.admin.Controllers" }
            );
            context.MapRoute(
                "admin_default_en",
                "en/admin/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TokenShop.Areas.admin.Controllers" }
            );
        }
    }
}