using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace TokenShop.Controlers
{
    public class RememberMeController : BaseController
    {
        // GET: RememberMe
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("remember_me");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username)
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("remember_me");
            ViewBag.message = "";
            try
            {
                if (string.IsNullOrEmpty(username))
                    ViewBag.message = "لطفا نام کاربری و یا ایمیل خود را وارد نمایید";
                if (string.IsNullOrEmpty(ViewBag.message))
                {
                    var data = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);

                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("UserName", username);
                    List<Customer> c = new Customer().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        Setting set = new Setting();
                        set.Id = 1;
                        set.GetOne();
                        Customer customer = c[0];
                        customer.PasswordResetHash = hashedPassword;
                        customer.Update();
                        string HtmlMessage = "<a href='"+set.SiteDomain+"/rememberme/reset/"+hashedPassword+ "' target='_blank'>" + set.SiteDomain + "/rememberme/reset/" + hashedPassword + "</a>";
                        TokenShop.Common.MyMail.Send("لینک بازنشانی کلمه عبور", HtmlMessage, customer.Email,set);
                        return RedirectToAction("send", "RememberMe");
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
        public ActionResult Send()
        {
            ViewBag.Title = "ارسال لینک بازنشانی کلمه عبور";
            return View();
        }
        public ActionResult Reset(string token)
        {
            ViewBag.token = token;
            ViewBag.Title = "بازنشانی کلمه عبور";
            ViewBag.change = false;
            return View();
        }
        [HttpPost]
        public ActionResult Reset(string password, string rePassword,string token)
        {
            ViewBag.Title = "بازنشانی کلمه عبور";
            ViewBag.message = "";
            try
            {
                if (string.IsNullOrEmpty(token))
                    ViewBag.message = "درخواست معتبر نمی باشد";
                if (string.IsNullOrEmpty(password))
                    ViewBag.message = "کلمات عبور وارد شده یکسان نمی باشند";
                if (string.IsNullOrEmpty(password))
                    ViewBag.message = "لطفا کلمه عبور جدید خود را وارد نمایید";
                if (string.IsNullOrEmpty(ViewBag.message))
                {
                    var data = Encoding.ASCII.GetBytes(password);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);

                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("PasswordResetToken", token);
                    List<Customer> c = new Customer().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        Customer customer = c[0];
                        customer.PasswordResetHash = "";
                        customer.Password = hashedPassword;
                        customer.Update();
                        ViewBag.change = false;
                    }
                    else
                        ViewBag.message = "درخواست معتبر نمی باشد";
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