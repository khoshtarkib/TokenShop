using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class TicketController : BaseController
    {
        // GET: admin/Ticket
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[ticket]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "تیکت ها";
            List<Ticket> tk = new List<Ticket>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Status", "1");
                tk = new Ticket().GetAll(parameters);
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
            if (HasPermission("[ticket]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;
            ViewBag.Title = "مشاهده تیکت";
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("TicketId", Id.ToString());
            List<TicketContent> tk = new TicketContent().GetAll(parameter);
            ViewBag.Id = Id;
            return View(tk);
        }
        [HttpPost]
        public ActionResult Display(string ticketId, string message)
        {
            string permissions = "";
            int stat = 0;
            if (HasPermission("[ticket]", ref permissions, ref stat))
                return RedirectToAction("AccessDeny");
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;
            ViewBag.Title = "مشاهده تیکت";
            try
            {
                Ticket tk = new Ticket();
                tk.Id = long.Parse(ticketId);
                tk = tk.GetOne();
                tk.Status = 2;
                tk.StatusDate = DateTime.Now;
                tk.Update();

                TicketContent tkc = new TicketContent();
                tkc.Content = message;
                tkc.UserId = int.Parse(Session["user_id"].ToString());
                tkc.RegisterDate = DateTime.Now;
                tkc.TicketId = tk.Id;
                tkc.Add();

                return RedirectToAction("Index", "Ticket");
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
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("TicketId", ticketId);
            List<TicketContent> model = new TicketContent().GetAll(parameter);
            ViewBag.Id = ticketId;
            return View(model);
        }
    }
}