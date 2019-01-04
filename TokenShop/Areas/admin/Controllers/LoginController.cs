using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;
using TokenShop.Models;
using System.Web;

namespace TokenShop.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            ViewBag.message = "";
            try
            {
                if (string.IsNullOrEmpty(email))
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
                    parameters.Add("UserName", email);
                    parameters.Add("Password", hashedPassword);
                    List<UserAccount> c = new UserAccount().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        HttpCookie mycookie = new HttpCookie("mapper2");
                        mycookie.Value = c[0].Id.ToString();
                        mycookie.Expires = DateTime.Now.AddMonths(1);
                        HttpContext.Response.Cookies.Add(mycookie);

                        Session.Add("user_id", c[0].Id);
                        Session.Add("user_fullname", c[0].FirstName + " " + c[0].LastName);
                        return RedirectToAction("Index", "Home");
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