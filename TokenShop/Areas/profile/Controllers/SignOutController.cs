using System.Web.Mvc;
using System.Web;
using System;

namespace TokenShop.Areas.profile.Controllers
{
    public class SignOutController : Controller
    {
        // GET: profile/SignOut
        public ActionResult Index()
        {
            var response = HttpContext.Response;
            response.Cookies.Remove("mapper");

            HttpCookie mycookie = new HttpCookie("mapper");
            mycookie.Expires = DateTime.Now.AddMinutes(-1);
            HttpContext.Response.Cookies.Add(mycookie);

            if (Session["customer_id"] != null)
                Session.Remove("customer_id");
            if (Session["customer_fullname"] != null)
                Session.Remove("customer_fullname");
            return RedirectToAction("Index", "SignIn", new { area = "" });
        }
    }
}