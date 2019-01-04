using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Areas.admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            string permissions = "";
            int stat = 0;
            HasPermission("[]", ref permissions, ref stat);
            ViewBag.permissions = permissions;
            ViewBag.stat = stat;

            ViewBag.Title = "داشبرد";            
            return View();
        }
        public PartialViewResult DashboardNewCustomers()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Count", "10");
            return PartialView("_partials/NewCustomersWidget", new Customer().GetAll(parameters));
        }
        public PartialViewResult DashboardNewTickets()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Status", "1");
            return PartialView("_partials/DashboardNewTickets", new Ticket().GetAll(parameters));
        }
        public PartialViewResult DashboardNewPayRequest()
        {
            List<PayRequest> tk = new List<PayRequest>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("status", "1");
            tk = new PayRequest().GetAll(parameters);
            return PartialView("_partials/DashboardNewPayRequest", tk);
        }
        public PartialViewResult DashboardNewBuyRequest()
        {
            List<BankReceipt> requests = null;
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("recipt_type", "1");
            parameter.Add("status", "1");            
            requests = new BankReceipt().GetAll(parameter);
            return PartialView("_partials/DashboardNewBuyRequest", requests);
        }
    }
}