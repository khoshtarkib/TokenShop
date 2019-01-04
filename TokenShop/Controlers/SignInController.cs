using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace TokenShop.Controlers
{
    public class SignInController : BaseController
    {
        // GET: SignIn
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("sign_in");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("sign_in");
            ViewBag.message = "";
            try
            {
                if (string.IsNullOrEmpty(username))
                    ViewBag.message = "لطفا نام کاربری خود را وارد نمایید";
                if (string.IsNullOrEmpty(password))
                    ViewBag.message = "لطفا کلمه عبور مد نظر خود را وارد نمایید";
                if (string.IsNullOrEmpty(ViewBag.message))
                {
                    var data = Encoding.ASCII.GetBytes(password);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);

                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("UserName", username);
                    parameters.Add("Password", hashedPassword);
                    List<Customer> c = new Customer().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        HttpCookie mycookie = new HttpCookie("mapper");
                        mycookie.Value = c[0].Id.ToString();
                        mycookie.Expires = DateTime.Now.AddMonths(1);
                        HttpContext.Response.Cookies.Add(mycookie);

                        Session.Add("customer_id", c[0].Id);
                        Session.Add("customer_code", c[0].UserCode);
                        Session.Add("customer_fullname", c[0].FirstName + " " + c[0].LastName);
                        return Redirect("/" + TokenShop.Common.LanguageManagment.lang + "/profile/");
                        //return RedirectToAction("Index", "Home", new { area = "profile" });
                    }
                    else
                        ViewBag.message = "کاربری با این مشخصات یافت نشد";
                }
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Description = ex.Message;
                if (ex.InnerException != null)
                    log.Description += ";" + ex.Message;
                log.ErrorDate = DateTime.Now;
                log.Add();
            }
            return View();
        }
    }
}