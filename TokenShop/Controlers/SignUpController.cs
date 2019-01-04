using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using TokenShop.Models;
using System.Collections.Generic;

namespace TokenShop.Controlers
{
    public class SignUpController : BaseController
    {
        // GET: SignUp
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("sign_up");
            return View();
        }
        public string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }
        [HttpPost]
        public ActionResult Index(string fname,string lname,string email,string password,string re_password)
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("sign_up");
            try {
                ViewBag.message = "";
                try {
                    if (string.IsNullOrEmpty(password))
                        ViewBag.message = "لطفا کلمه عبور مد نظر خود را وارد نمایید";
                    if (string.IsNullOrEmpty(email))
                        ViewBag.message = "لطفا ایمیل خود را وارد نمایید";
                    if (string.IsNullOrEmpty(lname))
                        ViewBag.message = "لطفا نام خانوادگی خود را وارد نمایید";
                    if (string.IsNullOrEmpty(fname))
                        ViewBag.message = "لطفا نام خود را وارد نمایید";

                    if (string.IsNullOrEmpty(ViewBag.message))
                    {
                        Customer c = new Customer();
                        Dictionary<string, string> parameter = new Dictionary<string, string>();
                        parameter.Add("UserName", email);
                        if(c.GetAll(parameter).Count>0)
                            ViewBag.message = "ایمیل وارد شده قبلا استفاده شده است";

                        if (string.IsNullOrEmpty(ViewBag.message))
                        {
                            var data = Encoding.ASCII.GetBytes(password);
                            var md5 = new MD5CryptoServiceProvider();
                            var md5data = md5.ComputeHash(data);
                            var hashedPassword = Encoding.ASCII.GetString(md5data);

                            c.UserName = email;
                            c.Email = email;
                            c.FirstName = fname;
                            c.LastName = lname;
                            c.BirthDate = null;
                            c.RegisterDate = DateTime.Now;
                            c.Password = hashedPassword;
                            c.UserCode = Get8Digits();
                            c.State = 1;
                            c.Add();
                            Session.Add("customer_code", c.UserCode);
                            Session.Add("customer_id", c.Id);
                            Session.Add("customer_fullname", c.FirstName + " " + c.LastName);
                            return RedirectToAction("Index", "Home", new { area = "profile" });
                        }
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
            }
            catch (Exception ex) {
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