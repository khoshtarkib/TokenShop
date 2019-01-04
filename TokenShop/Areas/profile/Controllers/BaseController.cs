using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.profile.Controllers
{
    public class BaseController : Controller
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
                    else
                    {
                        filterContext.Result = RedirectToAction("Index", "SignIn", new { area = "" });
                    }
                }
                else
                {
                    filterContext.Result = RedirectToAction("Index", "SignIn", new { area = "" });
                }
            }
            string[] path = Request.RawUrl.ToString().Split(new string[] { "/" }, System.StringSplitOptions.None);
            try {
                if (path[1].ToLower()=="en")
                    TokenShop.Common.LanguageManagment.lang = "en";
                else
                    TokenShop.Common.LanguageManagment.lang = "fa";
            }
            catch { }
            base.OnActionExecuting(filterContext);
        }
    }
}