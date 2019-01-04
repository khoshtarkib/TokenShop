using System.Web.Mvc;

namespace TokenShop.Controlers
{
    public class AboutUsController : BaseController
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("about_us");
            return View();
        }
    }
}