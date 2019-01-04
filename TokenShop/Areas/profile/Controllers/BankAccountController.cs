using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Areas.profile.Controllers
{
    public class BankAccountController : BaseController
    {
        // GET: profile/BankAccount
        public ActionResult Index()
        {
            ViewBag.Title = "کارت/حساب های بانکی";
            List<BankAccount> accounts=null;
            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("customer_id", Session["customer_id"].ToString());
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

        public ActionResult New()
        {
            ViewBag.Title = "ثبت حساب/کارت بانکی جدید";
            BankAccount tk = new BankAccount();
            return View(tk);
        }
        [HttpPost]
        public ActionResult New(string sheba, string cart, string number, string bank)
        {
            ViewBag.Title = "ثبت حساب/کارت بانکی جدید";
            BankAccount tk = new BankAccount();
            try
            {
                tk.CustomerId = long.Parse(Session["customer_id"].ToString());
                tk.BankId = int.Parse(bank);
                tk.Status = 1;
                tk.Sheba = sheba;
                tk.CardNumber = cart;
                tk.AccountNumber = number;
                tk.Add();

                return RedirectToAction("Index", "BankAccount");
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
        public ActionResult Modify(long Id)
        {
            ViewBag.Title = "ویرایش حساب/کارت بانکی جدید";
            BankAccount tk = new BankAccount();
            tk.Id = Id;
            tk = tk.GetOne();
            return View(tk);
        }
        [HttpPost]
        public ActionResult Modify(string bankId,string sheba, string cart, string number, string bank)
        {
            ViewBag.Title = "ویرایش حساب/کارت بانکی جدید";
            BankAccount tk = new BankAccount();
            try
            {
                tk.Id = long.Parse(bankId);
                tk = tk.GetOne();
                tk.BankId = int.Parse(bank);
                tk.Status = 1;
                tk.Sheba = sheba;
                tk.CardNumber = cart;
                tk.AccountNumber = number;
                tk.Update();

                return RedirectToAction("Index", "BankAccount");
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