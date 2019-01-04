using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class TokenController : BaseController
    {
        // GET: admin/Token

        public ActionResult Types()
        {
            ViewBag.Title = "انواع توکن ها";
            List<TokenType> tk = new List<TokenType>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                tk = new TokenType().GetAll(parameters);
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
        public ActionResult Ctype()
        {
            ViewBag.Title = "ثبت نوع توکن جدید";
            TokenType tk = new TokenType();
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
        public ActionResult Ctype(string title, string real, string toman, string description)
        {
            ViewBag.Title = "ثبت نوع توکن جدید";
            TokenType tk = new TokenType();
            try
            {
                tk.Title = title;
                tk.RealValue = long.Parse(real);
                tk.TomanValue = long.Parse(toman);
                tk.Description = description;
                tk.Add();
                return RedirectToAction("Types", "Token");
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
        public ActionResult Mtype(int Id)
        {
            ViewBag.Title = "ویرایش نوع توکن";
            TokenType tk = new TokenType();
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
        public ActionResult Mtype(string typeId, string title, string real, string toman, string description)
        {
            ViewBag.Title = "ویرایش نوع توکن";
            TokenType tk = new TokenType();
            try
            {
                tk.Id = int.Parse(typeId);
                tk = tk.GetOne();
                tk.Title = title;
                tk.RealValue = long.Parse(real);
                tk.TomanValue = long.Parse(toman);
                tk.Description = description;
                tk.Update();
                return RedirectToAction("Types", "Token");
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