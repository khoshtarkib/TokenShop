using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class TransactionsController : BaseController
    {
        // GET: admin/Transactions
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[transactions]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "گزارش تراکنش های بانکی";
            List<BankTransactions> tk = new List<BankTransactions>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new BankTransactions().GetAll(parameters);
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