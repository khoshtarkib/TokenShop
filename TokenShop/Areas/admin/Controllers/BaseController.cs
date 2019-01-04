using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public abstract class BaseController : Controller
    {
        public bool HasPermission(string permission,ref string per,ref int state)
        {
            if (Session["user_id"] == null)
            {
                return true;
            }
            UserAccount uc = new UserAccount();
            uc.Id = int.Parse(Session["user_id"].ToString());
            uc = uc.GetOne();
            per = uc.Permission;
            state = uc.State;
            if (uc.State == 10)
                return false;

            if (uc.Permission.Contains(permission))
                return false;
            return true;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user_id"] == null)
            {
                if (HttpContext.Request.Cookies["mapper2"] != null)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("Id", HttpContext.Request.Cookies["mapper2"].Value.ToString());
                    List<UserAccount> c = new UserAccount().GetAll(parameters);
                    if (c.Count > 0)
                    {
                        Session.Add("user_id", c[0].Id);
                        Session.Add("user_fullname", c[0].FirstName + " " + c[0].LastName);
                    }
                    else
                    {
                        filterContext.Result = RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    filterContext.Result = RedirectToAction("Index", "Login");
                }
            }
            string[] path = Request.RawUrl.ToString().Split(new string[] { "/" }, System.StringSplitOptions.None);
            try
            {
                if (path[1].ToLower() == "en")
                    TokenShop.Common.LanguageManagment.lang = "en";
                else
                    TokenShop.Common.LanguageManagment.lang = "fa";
            }
            catch { }
            ViewBag.UserFullName = Session["user_fullname"];
            base.OnActionExecuting(filterContext);
        }
    }
}