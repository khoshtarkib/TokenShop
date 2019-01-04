using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class LogsController : BaseController
    {
        // GET: admin/Logs
        public ActionResult Index(int? page)
        {
            string permissions = "";
            int stat = 0;
            HasPermission("[logs]",ref permissions, ref stat);
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "لاگ های سیستمی";
            List<ErrorLog> tk = new List<ErrorLog>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new ErrorLog().GetAll(parameters);
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