using System.Web.Mvc;
using TokenShop.Models;
using System.Collections.Generic;

namespace TokenShop.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["customer_id"] == null)
            {
                if (HttpContext.Request.Cookies["mapper"] != null)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("Id", HttpContext.Request.Cookies["mapper"].Value.ToString());
                    List<Customer> c = new Customer().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        Session.Add("customer_id", c[0].Id);
                        Session.Add("customer_code", c[0].UserCode);
                        Session.Add("customer_fullname", c[0].FirstName + " " + c[0].LastName);
                    }
                }
            }
            string[] path = Request.RawUrl.ToString().Split(new string[] { "/" }, System.StringSplitOptions.None);
            try
            {
                if (path[1].ToLower() == "en")
                    TokenShop.Common.LanguageManagment.lang = "en";
                else
                    TokenShop.Common.LanguageManagment.lang = "fa";
            }
            catch { }
            base.OnActionExecuting(filterContext);
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("home");
            Dictionary<string, string> paramter = new Dictionary<string, string>();
            paramter.Add("type", "1");
            paramter.Add("status", "1");
            List<Post> model = new Post().GetAll(paramter);

            Setting tk = new Setting();
            tk.Id = 1;
            tk = tk.GetOne();
            ViewBag.Slogan = tk.Slogan;
            
            return View(model);
        }
        public PartialViewResult TopSlider()
        {
            Dictionary<string, string> paramter = new Dictionary<string, string>();
            paramter.Add("type", "3");
            paramter.Add("status", "1");
            List<Post> posts = new Post().GetAll(paramter);
            return PartialView("_partials/TopSlider", posts);
        }
    }
}