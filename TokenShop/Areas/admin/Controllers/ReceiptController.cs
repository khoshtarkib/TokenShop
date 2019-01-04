using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Areas.admin.Controllers
{
    public class ReceiptController : BaseController
    {
        // GET: admin/Receipt
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[receipt]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "فیش های بانکی";
            List<BankReceipt> requests = null;
            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                requests = new BankReceipt().GetAll(parameter);
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
        public ActionResult Display(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[receipt]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;
            ViewBag.Title = "اطلاعات فیش پرداختی";
            BankReceipt tk = new BankReceipt();
            try
            {
                tk.Id = Id;
                tk = tk.GetOne();
                Order ord = new Order();
                ord.Id = tk.OrderId;
                ord = ord.GetOne();
                ViewBag.Count = ord.Count;
            //    if (tk.Statuse==2)
             //   {
              //      return RedirectToAction("", "Receipt");
               //d }
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
        public ActionResult Display(string receiptId, string status, string receiptNumber)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[receipt]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;
            ViewBag.Title = "اطلاعات فیش پرداختی";
            BankReceipt tk = new BankReceipt();
            try
            {
                tk.Id = long.Parse(receiptId);
                tk = tk.GetOne();
                tk.ReceiptNumber = receiptNumber;
                tk.Statuse = int.Parse(status);
                tk.Update();
                if (tk.Statuse==2)
                {
                    Order ord = new Order();
                    ord.Id = tk.OrderId;
                    ord = ord.GetOne();
                    CustomerToken token = new CustomerToken();
                    token.Count = ord.Count;
                    token.CustomerId = tk.CustomerId;
                    token.OrderId = tk.OrderId;
                    token.RialPrice = ord.Rial;
                    token.DollarPrice = ord.Dollar;
                    token.UroPrice = ord.Uro;
                    token.PondPrice = ord.Pond;
                    token.Action = 1;
                    token.ActionBy = 1;
                    token.ActionCustomerId = 0;
                    token.Date = DateTime.Now;
                    token.Add();
                }
                return RedirectToAction("", "Receipt");
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