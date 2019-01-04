using TokenShop.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace TokenShop.Areas.profile.Controllers
{
    public class TicketController : BaseController
    {
        // GET: profile/Ticket
        public ActionResult Index()
        {
            ViewBag.Title = "تیکتها";
            List<Ticket> tk = new List<Ticket>();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("CustomerId", Session["customer_id"].ToString());
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
        public ActionResult New()
        {
            ViewBag.Title = "ارسال تیکت جدید";
            Ticket tk = new Ticket();
            return View(tk);
        }
        [HttpPost]
        public ActionResult New(string subject,string message,string periority)
        {
            ViewBag.Title = "ارسال تیکت جدید";
            Ticket tk = new Ticket();
            try
            {
                tk.CreateDate = DateTime.Now;
                tk.CustomerId = long.Parse(Session["customer_id"].ToString());
                tk.Priority = int.Parse(periority);
                tk.Status = 1;
                tk.StatusDate = DateTime.Now;
                tk.Subject = subject;
                tk.Add();

                TicketContent tkc = new TicketContent();
                tkc.Content = message;
                tkc.CustomerId= long.Parse(Session["customer_id"].ToString());
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
            return View(tk);
        }
        public ActionResult Display(long Id)
        {
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
            ViewBag.Title = "مشاهده تیکت";
            try
            {
                Ticket tk = new Ticket();
                tk.Id = long.Parse(ticketId);
                tk = tk.GetOne();
                tk.Status = 1;
                tk.StatusDate = DateTime.Now;
                tk.Update();

                TicketContent tkc = new TicketContent();
                tkc.Content = message;
                tkc.CustomerId = long.Parse(Session["customer_id"].ToString());
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