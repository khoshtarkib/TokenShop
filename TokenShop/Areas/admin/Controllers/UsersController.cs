using System.Collections.Generic;
using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Text;
using System.Security.Cryptography;

namespace TokenShop.Areas.admin.Controllers
{
    public class UsersController : BaseController
    {
        // GET: admin/Users
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[users]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "کاربران";
            List<UserAccount> tk = new List<UserAccount>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new UserAccount().GetAll(parameters);
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
        public ActionResult Create()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[users]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت کاربر جدید";
            UserAccount tk = new UserAccount();
            try
            {
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
        public ActionResult Create(string userName,string password, string firstName, string lastName, string state, string address, string email,string phone, string mobilePhone,
            string cart, string concatus, string customers, string news, string faq, string slider, string ticket, string order, string payrequest, string receipt, string transactions, string users, string settings)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[users]", ref permissions,ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "اطلاعات کاربر";
            UserAccount tk = new UserAccount();
            try
            {
                System.Collections.Specialized.NameValueCollection val=Request.Form;
                var data = Encoding.ASCII.GetBytes(password);
                var md5 = new MD5CryptoServiceProvider();
                var md5data = md5.ComputeHash(data);
                var hashedPassword = Encoding.ASCII.GetString(md5data);

                string permission = (!string.IsNullOrEmpty(cart) ? "[cart]" : "");
                permission += (!string.IsNullOrEmpty(concatus) ? "[concatus]" : "");
                permission += (!string.IsNullOrEmpty(customers) ? "[customers]" : "");
                permission += (!string.IsNullOrEmpty(news) ? "[news]" : "");
                permission += (!string.IsNullOrEmpty(faq) ? "[faq]" : "");
                permission += (!string.IsNullOrEmpty(slider) ? "[slider]" : "");
                permission += (!string.IsNullOrEmpty(ticket) ? "[ticket]" : "");
                permission += (!string.IsNullOrEmpty(order) ? "[order]" : "");
                permission += (!string.IsNullOrEmpty(payrequest) ? "[payrequest]" : "");
                permission += (!string.IsNullOrEmpty(receipt) ? "[receipt]" : "");
                permission += (!string.IsNullOrEmpty(transactions) ? "[transactions]" : "");
                permission += (!string.IsNullOrEmpty(users) ? "[users]" : "");
                permission += (!string.IsNullOrEmpty(settings) ? "[settings]" : "");

                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.State = int.Parse(state);
                tk.Address = address;
                tk.Email = email;
                tk.UserName = userName;
                tk.Password = hashedPassword;
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.RegisterDate = DateTime.Now;
                tk.Permission = permission;
                tk.Add();
                return RedirectToAction("", "Users");
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
        public ActionResult Modify(int Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[users]",ref permissions,ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Profile = false;
            ViewBag.Title = "ثبت کاربر جدید";
            UserAccount tk = new UserAccount();
            try
            {
                tk.Id = Id;
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
        public ActionResult Modify(string userName, string password, string userId, string firstName, string lastName, string state, string address, string email, string phone, string mobilePhone
            ,string cart, string concatus, string customers, string news, string faq, string slider, string ticket, string order, string payrequest, string receipt, string transactions, string users, string settings)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[users]",ref permissions,ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "اطلاعات کاربر";            
            UserAccount tk = new UserAccount();
            try
            {


                string permission = (!string.IsNullOrEmpty(cart) ? "[cart]" : "");
                permission += (!string.IsNullOrEmpty(concatus) ? "[concatus]" : "");
                permission += (!string.IsNullOrEmpty(customers) ? "[customers]" : "");
                permission += (!string.IsNullOrEmpty(news) ? "[news]" : "");
                permission += (!string.IsNullOrEmpty(faq) ? "[faq]" : "");
                permission += (!string.IsNullOrEmpty(slider) ? "[slider]" : "");
                permission += (!string.IsNullOrEmpty(ticket) ? "[ticket]" : "");
                permission += (!string.IsNullOrEmpty(order) ? "[order]" : "");
                permission += (!string.IsNullOrEmpty(payrequest) ? "[payrequest]" : "");
                permission += (!string.IsNullOrEmpty(receipt) ? "[receipt]" : "");
                permission += (!string.IsNullOrEmpty(transactions) ? "[transactions]" : "");
                permission += (!string.IsNullOrEmpty(users) ? "[users]" : "");
                permission += (!string.IsNullOrEmpty(settings) ? "[settings]" : "");

                tk.Id = int.Parse(userId);
                tk = tk.GetOne();
                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.State = int.Parse(state);
                tk.Address = address;
                tk.Email = email;
                tk.UserName = userName;
                tk.Permission = permission;
                if (!string.IsNullOrEmpty(password))
                {
                    var data = Encoding.ASCII.GetBytes(password);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);
                    tk.Password = hashedPassword;
                }
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.Update();
                return RedirectToAction("", "Users");
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
        public ActionResult My()
        {
            string permissions = "";
            int stat = 0;
            HasPermission("",ref permissions,ref stat);
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Profile = true;
            ViewBag.Title = "ثبت کاربر جدید";
            UserAccount tk = new UserAccount();
            try
            {
                tk.Id = int.Parse(Session["user_id"].ToString());
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
            return View("Modify",tk);
        }
        [HttpPost]
        public ActionResult My(string userName, string password, string firstName, string lastName, string address, string email, string phone, string mobilePhone)
        {
            string permissions = "";
            int stat = 0;
            HasPermission("",ref permissions,ref stat);
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Profile = true;
            ViewBag.Title = "اطلاعات کاربر";
            UserAccount tk = new UserAccount();
            try
            {
                tk.Id = int.Parse(Session["user_id"].ToString());
                tk = tk.GetOne();
                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.Address = address;
                tk.Email = email;
                tk.UserName = userName;
                if (!string.IsNullOrEmpty(password))
                {
                    var data = Encoding.ASCII.GetBytes(password);
                    var md5 = new MD5CryptoServiceProvider();
                    var md5data = md5.ComputeHash(data);
                    var hashedPassword = Encoding.ASCII.GetString(md5data);
                    tk.Password = hashedPassword;
                }
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.Update();
                return RedirectToAction("", "Users");
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
            return View("Modify",tk);
        }
    }
}