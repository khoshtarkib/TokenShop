using System.Web.Mvc;

namespace TokenShop.Areas.profile
{
    public class profileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "profile_default",
                "profile/{controller}/{action}/{id}",
                new { controller="home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TokenShop.Areas.profile.Controllers" }
            );
            context.MapRoute(
                "profile_default_fa",
                "fa/profile/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TokenShop.Areas.profile.Controllers" }
            );
            context.MapRoute(
                "profile_default_en",
                "en/profile/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TokenShop.Areas.profile.Controllers" }
            );
        }
    }
}