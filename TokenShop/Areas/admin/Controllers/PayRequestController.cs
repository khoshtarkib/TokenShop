using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class PayRequestController : BaseController
    {
        // GET: admin/PayRequest
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[payrequest]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "درخواست های واریز";
            List<PayRequest> tk = new List<PayRequest>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new PayRequest().GetAll(parameters);
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
        public ActionResult View(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[payrequest]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "درخواست های واریز";
            PayRequest tk = new PayRequest();
            try
            {
                tk.Id = Id;
                tk = tk.GetOne();
                if (tk.Status == 2)
                    return RedirectToAction("Index");


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
        public ActionResult View(long Id,string status)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[payrequest]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "درخواست های واریز";
            PayRequest tk = new PayRequest();
            try
            {
                tk.Id = Id;
                tk = tk.GetOne();
                tk.Status = int.Parse(status);
                if (status == "2")
                    tk.PayDate = DateTime.Now;
                tk.Update();
               
                CustomerToken token = new CustomerToken();
                token.Count = tk.Count;
                token.CustomerId = tk.CustomerId;
                token.PayRequestId = tk.Id;
                token.RialPrice = tk.Rial;
                token.DollarPrice = tk.Dollar;
                token.UroPrice = tk.Uro;
                token.PondPrice = tk.Pond;
                token.Action = 2;
                token.ActionBy = 4;
                token.ActionCustomerId = 0;
                token.Date = DateTime.Now;
                token.Add();
                return RedirectToAction("Index");
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