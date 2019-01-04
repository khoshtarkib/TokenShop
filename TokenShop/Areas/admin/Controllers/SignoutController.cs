using System.Web.Mvc;
using System.Web;
using System;


namespace TokenShop.Areas.admin.Controllers
{
    public class SignoutController : Controller
    {
        // GET: admin/Signout
        public ActionResult Index()
        {
            var response = HttpContext.Response;
            response.Cookies.Remove("mapper2");

            HttpCookie mycookie = new HttpCookie("mapper2");
            mycookie.Expires = DateTime.Now.AddMinutes(-1);
            HttpContext.Response.Cookies.Add(mycookie);

            if (Session["user_id"] != null)
                Session.Remove("user_id");
            if (Session["user_fullname"] != null)
                Session.Remove("user_fullname");
            return RedirectToAction("Index", "Login");
        }
    }
}