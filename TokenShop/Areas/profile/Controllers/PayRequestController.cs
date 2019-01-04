using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Areas.profile.Controllers
{
    public class PayRequestController : BaseController
    {
        // GET: profile/PayRequest
        public ActionResult Index()
        {
            ViewBag.Title = "درخواست برداشت وجه";
            List<PayRequest> requests = null;
            try
            {
                Dictionary<string, string> parameter =new Dictionary<string, string>();
                parameter.Add("customer_id", Session["customer_id"].ToString());
                requests = new PayRequest().GetAll(parameter);                
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
            return View(requests);
        }

        public ActionResult New()
        {
            ViewBag.Title = "ثبت درخواست برداشت وجه";
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("status", "2");
            List<BankAccount> tk = new BankAccount().GetAll(parameter);
            if (tk.Count <= 0)
                ViewBag.WarningMessage = "شما هیج حساب/کارت بانکی معتبر و فعالی بر روی سیستم ثبت نکرده اید.لطفا از طریق منوی حساب/کارتهای بانکی نسبت به ثبت اطلاعات بانکی خود اقدام نمایید";
            return View(tk);
        }
        [HttpPost]
        public ActionResult New(string bankAccount, string price)
        {
            ViewBag.Title = "ثبت درخواست برداشت وجه";
            List<BankAccount> tk;
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            try
            {
                bool valid = true;
                long count = 0;
                try
                {
                    count = long.Parse(price);
                }
                catch { }
                Customer customer = new Customer();
                customer.Id = long.Parse(Session["customer_id"].ToString());
                customer = customer.GetOne();

                if (count > customer.TokenCount)
                {
                    ViewBag.DangerMessage = "موجودی فعلی حساب شما " + customer.TokenCount + " واحد می باشد";
                    valid = false;
                }

                ViewBag.WarningMessage = "";
                if (valid)
                {
                    parameter.Add("customer_id", Session["customer_id"].ToString());
                    List<PayRequest> requests = requests = new PayRequest().GetAll(parameter);
                    foreach(PayRequest req in requests)
                        if (req.Status==1)
                        {
                            ViewBag.DangerMessage = "شما یک درخواست برداشت وجه در تاریخ "+req.RequestDatePersian+" داشته اید که هنوز بررسی نشده است.شما اجازه ثبت همزمان 2 درخواست برداشت وجه را ندارید.لطفا تا وصول نتیجه درخواست قبلی منتظر بمانید";
                            valid = false;
                        }
                    if (valid)
                    {
                        Setting set = new Setting();
                        set.Id = 1;
                        set = set.GetOne();
                        PayRequest tkc = new PayRequest();
                        tkc.CustomerId = long.Parse(Session["customer_id"].ToString());
                        tkc.RequestDate = DateTime.Now;
                        tkc.Status = 1;
                        tkc.BankAccountId = long.Parse(bankAccount);
                        tkc.Currency = 1;
                        tkc.Dollar = set.Dollar;
                        tkc.Count = count;
                        tkc.IncomePercent = set.GlobalIncomePercent;
                        tkc.IncomePrice = ((count * set.Rial) * tkc.IncomePercent/100);
                        tkc.Amount = (count * set.Rial) - tkc.IncomePrice;
                        tkc.Pond = set.Pond;
                        tkc.Rial = set.Rial;
                        tkc.Uro = set.Uro;
                        tkc.Add();

                        return RedirectToAction("Index", "PayRequest");
                    }
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
            parameter = new Dictionary<string, string>();
            parameter.Add("status", "2");
            tk = new BankAccount().GetAll();
            return View(tk);
        }
    }
}