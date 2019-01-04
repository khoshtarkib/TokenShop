using System.Web.Mvc;

namespace TokenShop.Areas.admin.Controllers
{
    public class AccessDenyController : BaseController
    {
        // GET: admin/AccessDeny
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            HasPermission("",ref permissions, ref stat);
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.DangerMessage = "شما مجوز دسترسی به این بخش از اطلاعات رو ندارید. جهت دسترسی به این بخش با مدیر کل سامانه تماس بگیرید";
            return View();
        }
    }
}