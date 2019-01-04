using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Areas.admin.Controllers
{
    public class CartController : BaseController
    {
        // GET: profile/BankAccount
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[cart]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "کارت/حساب های بانکی";
            List<BankAccount> accounts = null;
            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                accounts = new BankAccount().GetAll(parameter);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Description = ex.Message;
                if (ex.InnerException != null)
                    log.Description += ";" + ex.Message;
                log.ErrorDate = DateTime.Now;
                log.CustomerId = long.Parse(Session["customer_id"].ToString());
                log.Add();
            }
            return View(accounts);
        }
        public ActionResult Modify(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[cart]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ویرایش حساب/کارت بانکی";
            BankAccount tk = new BankAccount();
            tk.Id = Id;
            tk = tk.GetOne();
            return View(tk);
        }
        [HttpPost]
        public ActionResult Modify(string bankId, string sheba, string cart, string number, string bank,string status)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[cart]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ویرایش حساب/کارت بانکی ";
            BankAccount tk = new BankAccount();
            try
            {
                tk.Id = long.Parse(bankId);
                tk = tk.GetOne();
                tk.BankId = int.Parse(bank);
                tk.Status = int.Parse(status);
                tk.Sheba = sheba;
                tk.CardNumber = cart;
                tk.AccountNumber = number;
                tk.Update();

                return RedirectToAction("Index", "Cart");
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