using System;
using System.Collections.Generic;
using TokenShop.Models;
using System.Web.Mvc;

namespace TokenShop.Areas.admin.Controllers
{
    public class ContactUsController : BaseController
    {
        // GET: admin/ContactUs
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[concatus]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "درخواست های تماس";
            List<ContactUs> tk = new List<ContactUs>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new ContactUs().GetAll(parameters);
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
        public ActionResult Display(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[concatus]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "اطلاعات درخواست تماس";
            ContactUs tk = new ContactUs();
            try
            {
                tk.Id = Id;
                tk = tk.GetOne();
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