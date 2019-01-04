using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class OrdersController : BaseController
    {
        // GET: admin/Orders
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[order]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "درخواست های خرید";
            List<Order> tk = new List<Order>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new Order().GetAll(parameters);
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