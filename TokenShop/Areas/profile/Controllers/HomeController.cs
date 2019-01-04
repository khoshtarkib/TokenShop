using TokenShop.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TokenShop.Areas.profile.Controllers
{
    public class HomeController : BaseController
    {
        // GET: profile/Home
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("dashboard");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Session["customer_id"].ToString());
            ViewBag.TicketCount = new Ticket().GetAll(parameters).Count;
            ViewBag.OpenTicketCount = 0;

            parameters = new Dictionary<string, string>();
            parameters.Add("CustomerId", Session["customer_id"].ToString());
            List<CustomerToken> tokens = new CustomerToken().GetAll(parameters);

            long sum = 0;
            long count = 0;
            foreach (CustomerToken c in tokens)
                if (c.Action == 1)
                {
                    sum += c.Count * c.RialPrice;
                    count += c.Count;
                }
            ViewBag.token_price_in = sum;
            ViewBag.token_count_in = count;

            sum = 0;
            count = 0;
            foreach (CustomerToken c in tokens)
                if (c.Action == 2)
                {
                    sum += c.Count * c.RialPrice;
                    count += c.Count;
                }
            ViewBag.token_price_out = sum;
            ViewBag.token_count_out = count;
            ViewBag.token_price = ViewBag.token_price_in - ViewBag.token_price_out;
            ViewBag.token_count = ViewBag.token_count_in - ViewBag.token_count_out;
            return View();
        }
    }
}