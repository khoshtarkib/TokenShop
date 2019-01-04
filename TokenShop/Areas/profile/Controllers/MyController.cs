using System;
using TokenShop.Models;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace TokenShop.Areas.profile.Controllers
{
    public class MyController : BaseController
    {
        // GET: profile/My
        public ActionResult Account()
        {
            ViewBag.Title = "اطلاعات حساب کاربری";
            Customer tk = new Customer();
            try
            {
                tk.Id = long.Parse(Session["customer_id"].ToString());
                tk = tk.GetOne();
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
            return View(tk);
        }
        [HttpPost]
        public ActionResult Account(string firstName, string lastName, string address, string email, string city, string webAddress
            , string phone, string mobilePhone, string nationalCode,string userName,string password)
        {
            ViewBag.Title = "اطلاعات حساب کاربری";
            Customer tk = new Customer();
            try
            {
                tk.Id = long.Parse(Session["customer_id"].ToString());
                tk = tk.GetOne();
                tk.UserName = userName;
                if (!string.IsNullOrEmpty(password))
                {
                    var data = Encoding.ASCII.GetBytes(password);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);
                    tk.Password = hashedPassword;
                }
                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.Address = address;
                tk.Email = email;
                tk.City = city;
                tk.WebAddress = webAddress;
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.NationalCode = nationalCode;
                tk.Update();
                ViewBag.SuccessMessage = "اطلاعات شما به روز رسانی گردید";
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
            return View(tk);
        }
    }
}