using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Controlers
{
    public class PaymentController : BaseController
    {
        // GET: Payment
        public ActionResult Pay(int Id)
        {
            return View();
            Order o = new Order();
            o.Id = Id;
            o = o.GetOne();
            return View();
            com.zarinpal.www.PaymentGatewayImplementationService client = new com.zarinpal.www.PaymentGatewayImplementationService();
            string authority;
            int amount = (int)o.Amount;
            string description = "خرید از سایت من";
            string email = "";
            string mobile = "";
            
            string callbackUrl = "/Payment/zp/verify?amount=" + amount;

            int status = client.PaymentRequest("", amount, description, email, mobile, callbackUrl, out authority);

            if (status == 100)
            {
                ////For release mode
                Response.Redirect("https://zarinpal.com/pg/StartPay/" + authority);

                ////For test mode
                //Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + authority);
            }
            return View();
        }
    }
}