using System.Web.Mvc;
using TokenShop.Models;
using System;

namespace TokenShop.Areas.admin.Controllers
{
    public class SettingController : BaseController
    {
        private int SettingId = 1;
        // GET: admin/Setting
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[settings]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "تنظیمات";
            Setting tk = new Setting();
            try
            {
                tk.Id = SettingId;
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
        [HttpPost]
        public ActionResult Index(string settingId, string siteTitle,string siteDomain,string siteStatus,string globalIncomePercent,string smtpAddress, string smtpPort
            ,string smtpUserName,string smtpPassword,string slogan,string rial,string dollar,string uro,string pond)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[settings]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;
            ViewBag.Title = "تنظیمات";
            Setting tk = new Setting();
            try
            {
                tk.Id = SettingId;
                tk = tk.GetOne();
                tk.Id = int.Parse(settingId);
                tk.GlobalIncomePercent = int.Parse(globalIncomePercent);
                tk.SiteDomain = siteDomain;
                tk.SiteStatus = int.Parse(siteStatus);
                tk.SiteTitle = siteTitle;
                tk.SmtpAddress = smtpAddress;
                tk.SmtpPassword = smtpPassword;
                tk.SmtpPort = int.Parse(smtpPort);
                tk.SmtpUserName = smtpUserName;
                try {
                    tk.Dollar = long.Parse(dollar);
                }
                catch
                {
                    tk.Dollar = 0;
                }
                try {
                    tk.Rial = long.Parse(rial);
                }
                catch
                {
                    tk.Rial = 0;
                }
                try {
                    tk.Pond = long.Parse(pond);
                }
                catch { 
                    tk.Pond=0;
                }
                try {
                    tk.Uro = long.Parse(uro);
                }
                catch
                {
                    tk.Uro = 0;
                }
                tk.Slogan = slogan.Replace("<br/>","\n");
                tk.Update();
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