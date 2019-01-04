using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class QuestionsController : BaseController
    {
        // GET: admin/News
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[faq]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "سوالات متداول";
            List<Post> tk = new List<Post>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("type", "2");
                tk = new Post().GetAll(parameters);
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
        public ActionResult Create()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[faq]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت سوال جدید";

            Post tk = new Post();
            try
            {
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
        public ActionResult Create(string subject, string content, string status,string point)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[faq]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت سوال جدید";
            Post tk = new Post();
            try
            {
                tk.Point = 0;
                try
                {
                    tk.Point = int.Parse(point);
                }
                catch { }
                tk.Type = 2;
                tk.CreateDate = DateTime.Now;
                tk.FriendlyUrl = subject.Replace("%", "").Replace("?", "").Replace(".", "").Replace("<", "").Replace(">", "").Replace(")", "").Replace("(", "").Replace("'", "").Replace("\"", "").Replace("\\", "").Replace("/", "").Replace("!", "").Replace("+", "").Replace("_", "").Replace("-", "").Replace("#", "").Replace("}", "").Replace("{", "").Replace("]", "").Replace("[", "").Trim().Replace(" ", "-");
                tk.ModifyDate = DateTime.Now;
                tk.PostContent = content;
                tk.Seen = 0;
                tk.Status = int.Parse(status);
                tk.Subject = subject;
                tk.UserId = int.Parse(Session["user_id"].ToString());
                tk.Add();
                return RedirectToAction("", "Questions");
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
        public ActionResult Modify(long Id)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[faq]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ثبت سوال جدید";
            Post tk = new Post();
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
        [HttpPost]
        public ActionResult Modify(string newsId, string subject, string content, string status, string point)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[faq]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "ویرایش سوال";
            Post tk = new Post();
            try
            {
                tk.Id = long.Parse(newsId);
                tk = tk.GetOne();
                try
                {
                    tk.Point = int.Parse(point);
                }
                catch { }
                tk.ModifyDate = DateTime.Now;
                tk.PostContent = content;
                tk.Status = int.Parse(status);
                tk.Subject = subject;
                tk.ModifiedUserId = int.Parse(Session["user_id"].ToString());
                tk.Update();
                return RedirectToAction("", "Questions");
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