using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;
using System.Text;

namespace TokenShop.Areas.admin.Controllers
{
    public class CustomersController : BaseController
    {
        // GET: admin/Customers
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[customers]",ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "مشتریان";
            List<Customer> tk = new List<Customer>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new Customer().GetAll(parameters);
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
        public ActionResult Display(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[customers]",ref permissions,ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "اطلاعات مشتری";
            Customer tk = new Customer();
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
        public ActionResult Display(string customerId,string firstName,string lastName,string state,string address,string email,string city,string webAddress
            ,string phone,string mobilePhone,string nationalCode)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[customers]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "اطلاعات مشتری";
            Customer tk = new Customer();
            try
            {
                tk.Id = long.Parse(customerId);
                tk = tk.GetOne();
                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.State = int.Parse(state);
                tk.Address = address;
                tk.Email = email;
                tk.City = city;
                tk.WebAddress = webAddress;
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.NationalCode = nationalCode;
                tk.Update();
                return RedirectToAction("", "Customers");
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

        public PartialViewResult Tickets(long Id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Id.ToString());
            return PartialView("_partials/Tickets", new Ticket().GetAll(parameters));
        }
        public PartialViewResult Orders(long Id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Id.ToString());
            return PartialView("_partials/Orders", new Order().GetAll(parameters));
        }
        public PartialViewResult Token(long Id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Id.ToString());
            return PartialView("_partials/Toekn", new CustomerToken().GetAll(parameters));
        }
        public PartialViewResult BankAccounts(long Id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("customer_id", Id.ToString());
            return PartialView("_partials/BankAccounts", new BankAccount().GetAll(parameters));
        }
        public PartialViewResult Receipt(long Id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("customer_id", Id.ToString());
            return PartialView("_partials/Receipt", new BankReceipt().GetAll(parameters));
        }
        public ActionResult New()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[customers]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت مشتری جدید";
            Customer tk = new Customer();
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
        public ActionResult New(string firstName, string lastName, string state, string address, string email, string city, string webAddress
            , string phone, string mobilePhone, string nationalCode,string password,string re_password)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[customers]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت مشتری جدید";
            Customer tk = new Customer();
            try
            {
                var data = Encoding.ASCII.GetBytes(password);
                var md5 = new MD5CryptoServiceProvider();
                var md5data = md5.ComputeHash(data);
                var hashedPassword = Encoding.ASCII.GetString(md5data);

                tk.UserName = email;
                tk.BirthDate = null;
                tk.RegisterDate = DateTime.Now;
                tk.Password = hashedPassword;

                tk.FirstName = firstName;
                tk.LastName = lastName;
                tk.State = int.Parse(state);
                tk.Address = address;
                tk.Email = email;
                tk.City = city;
                tk.WebAddress = webAddress;
                tk.Phone = phone;
                tk.MobilePhone = mobilePhone;
                tk.NationalCode = nationalCode;
                bool valid = true;
                if (string.IsNullOrEmpty(password))
                {
                    ViewBag.DangerMessage = "کلمه عبور را وارد نمایید";
                    valid = false;
                }
                if (password!=re_password)
                {
                    ViewBag.DangerMessage = "کلمات عبور وارد شده یکسان نمی باشند";
                    valid = false;
                }
                if (valid)
                    tk.Add();
                return RedirectToAction("", "Customers");
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