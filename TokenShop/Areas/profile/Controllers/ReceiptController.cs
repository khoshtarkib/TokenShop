using TokenShop.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace TokenShop.Areas.profile.Controllers
{
    public class ReceiptController : BaseController
    {
        // GET: profile/Receipt
        public ActionResult Index()
        {
            ViewBag.Title = "فیش های بانکی";
            List<BankReceipt> requests = null;
            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("customer_id", Session["customer_id"].ToString());
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
    }
}