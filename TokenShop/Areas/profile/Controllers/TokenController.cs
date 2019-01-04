using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace TokenShop.Areas.profile.Controllers
{
    public class TokenController : BaseController
    {
        // GET: profile/Token
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Transaction(int id=0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Session["customer_id"].ToString());
            parameters.Add("Action", id.ToString());
            
            return View(new CustomerToken().GetAll(parameters));
        }
        public ActionResult Buy()
        {
            ViewBag.Title = "خرید توکن";
            ViewBag.step = 1;
            return View();
        }
        [HttpPost]
        public ActionResult Buy(string money,string reciption_date,string reciption_bank,string reciption_number,string payMode,string descript,string step,string count,string income_price, HttpPostedFileBase cover)
        {
            ViewBag.Title = "خرید توکن";
            if (step == "1")
            {
                int c = 0;
                try
                {
                    c = int.Parse(count);
                }
                catch
                {
                }
                if (c<=0)
                {
                    ViewBag.step = 1;
                    ViewBag.DangerMessage = "لطفا تعداد توکن مورد نظر را وارد نمایید";
                }
                else
                {
                    ViewBag.step = 2;
                    ViewBag.count = c;
                    Setting set = new Setting();
                    set.Id = 1;
                    set = set.GetOne();

                    long income = ((c * set.Rial) * set.GlobalIncomePercent) / 100;                
                    ViewBag.rial = (c * set.Rial)+ income;

                    income = ((c * set.Uro) * set.GlobalIncomePercent) / 100;
                    ViewBag.uro = (c * set.Uro) + income;

                    income = ((c * set.Dollar) * set.GlobalIncomePercent) / 100;
                    ViewBag.dollar = (c * set.Dollar) + income;

                    income = ((c * set.Pond) * set.GlobalIncomePercent) / 100;
                    ViewBag.pond = (c * set.Pond) + income;

                    ViewBag.income_price = income;
                }
            }
            if (step == "2")
            {
                try
                {
                    bool valid = true;
                    if (string.IsNullOrEmpty(payMode))
                    {
                        ViewBag.DangerMessage = "لطفا نحوه پرداخت وجه را مشخص نمایید";
                        valid = false;
                    }
                    if (valid)
                    {
                        string attachment = "";
                        if (cover != null)
                        {
                            string rnd = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + Path.GetExtension(cover.FileName);
                            cover.SaveAs(Server.MapPath("~/media/receipt/" + rnd));
                            attachment = rnd;
                        }

                        Setting set = new Setting();
                        set.Id = 1;
                        set = set.GetOne();

                        Order o = new Order();
                        o.Currency = 1;
                        o.IncomePercent = set.GlobalIncomePercent;
                        o.IncomePrice = long.Parse(income_price);
                        o.Rial = set.Rial;
                        o.Uro = set.Uro;
                        o.Dollar = set.Dollar;
                        o.Pond = set.Pond;
                        o.Amount = long.Parse(money);
                        o.Count = long.Parse(count);
                        o.CreateDate = DateTime.Now;
                        o.ModifyDate = DateTime.Now;
                        o.CustomerId = long.Parse(Session["customer_id"].ToString());
                        o.Description = "";
                        o.Status = 1;
                        o.Add();
                        if (payMode == "1")
                        {
                            BankReceipt recipt = new BankReceipt();
                            recipt.Amount = o.Amount;
                            recipt.BankName = reciption_bank;
                            recipt.CustomerId = o.CustomerId;
                            recipt.Description = descript;
                            recipt.OrderId = o.Id;
                            recipt.PayDate = Common.PersianDate.ConvertPersianDateToEnglishDate(reciption_date);
                            recipt.ReciptType = 1;
                            recipt.Statuse = 1;
                            recipt.ReceiptNumber = reciption_number;
                            recipt.Attachment = attachment;
                            recipt.Add();
                            return RedirectToAction("done", "token");
                        }
                        if (payMode == "2")
                            return RedirectToAction("pay", "Payment", new { Id = o.Id, area = "" });
                    }
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
            }
            return View();
        }
        public ActionResult Done()
        {
            ViewBag.Title = "خرید توکن";
            return View();
        }
        public ActionResult Transfer()
        {
            ViewBag.Title = "انتقال توکن";
            ViewBag.state = 1;
            return View();
        }
        public ActionResult TransferDone()
        {
            ViewBag.Title = "انتقال توکن";
            return View();
        }
        [HttpPost]
        public ActionResult Transfer(string count,string reciver_code,string code,string state)
        {
            ViewBag.Title = "انتقال توکن";
            try
            {
                ViewBag.state = int.Parse(state);
                if (state == "2")
                {
                    reciver_code = code;
                }
                ViewBag.code = reciver_code;
                bool valid = true;
                Customer rc = new Customer();
                Dictionary<string, string> paramter = new Dictionary<string, string>();
                paramter.Add("UserCode", reciver_code);
                List<Customer> lst = rc.GetAll(paramter);
                if (lst.Count != 1)
                {
                    ViewBag.DangerMessage = "شخصی با شناسه مورد نظر شما یافت نشد";
                    valid = false;
                }
                else
                {
                    ViewBag.transfer_user_name = lst[0].FirstName + " " + lst[0].LastName;
                    ViewBag.state = 2;
                }
                rc = lst[0];
                
                Customer customer = new Customer();
                customer.Id = long.Parse(Session["customer_id"].ToString());
                customer = customer.GetOne();
                
                if (long.Parse(count) > customer.TokenCount)
                {
                    ViewBag.DangerMessage = "موجودی فعلی حساب شما " + customer.TokenCount + " واحد می باشد";
                    valid = false;
                }
                if (state == "1")
                {
                    valid = false;
                }
                if (valid)
                {
                    Setting set = new Setting();
                    set.Id = 1;
                    set = set.GetOne();

                    CustomerToken token = new CustomerToken();
                    token.Count = long.Parse(count);
                    token.CustomerId = rc.Id;
                    token.OrderId = 0;
                    token.RialPrice = set.Rial;
                    token.DollarPrice = set.Dollar;
                    token.UroPrice = set.Uro;
                    token.PondPrice = set.Pond;
                    token.Action = 1;
                    token.ActionBy = 3;
                    token.ActionCustomerId = customer.Id;
                    token.Date = DateTime.Now;
                    token.Add();

                    token = new CustomerToken();
                    token.Count = long.Parse(count);
                    token.CustomerId = customer.Id;
                    token.OrderId = 0;
                    token.RialPrice = set.Rial;
                    token.DollarPrice = set.Dollar;
                    token.UroPrice = set.Uro;
                    token.PondPrice = set.Pond;
                    token.Action = 2;
                    token.ActionBy = 2;
                    token.ActionCustomerId = rc.Id;
                    token.Date = DateTime.Now;
                    token.Add();

                    return RedirectToAction("TransferDone", "token");
                    
                }
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
            return View();
        }
    }
}