using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Controllers
{
    public class ContactUsController : Controller
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
        // GET: front/ContactUs
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("contact_us");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name, string subject, string email, string message)
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("contact_us");
            ViewBag.message = "";
            try
            {
                if (string.IsNullOrEmpty(message))
                    ViewBag.message = "لطفا متن پیام مورد نظر خود را وارد نمایید";
                if (string.IsNullOrEmpty(subject))
                    ViewBag.message = "لطفا موضوع مورد نظر را وارد نمایید";
                if (string.IsNullOrEmpty(email))
                    ViewBag.message = "لطفا ایمیل خود را وارد نمایید";
                if (string.IsNullOrEmpty(name))
                    ViewBag.message = "لطفا نام خود را وارد نمایید";
                if (string.IsNullOrEmpty(ViewBag.message))
                {
                    ContactUs cont = new ContactUs();
                    cont.Date = DateTime.Now;
                    cont.Email = email;
                    cont.Ip = "";
                    cont.Message = message;
                    cont.Name = name;
                    cont.Read = false;
                    cont.Subject = subject;
                    cont.Add();
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = "خطای سیستم رخ داده است.لطفا مجدد تلاش نمایید";
                ErrorLog err = new ErrorLog();
                err.Description = ex.Message;
                if (ex.InnerException != null)
                    err.Description += ";" + ex.InnerException.Message;
                err.Add();
            }
            return View();
        }
    }
}