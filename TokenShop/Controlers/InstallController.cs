using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System;
using TokenShop.Models;

namespace TokenShop.Controlers
{
    public class InstallController : BaseController
    {
        // GET: Install
        public ActionResult Index()
        {
            var data = Encoding.ASCII.GetBytes("1qaz@WSX");
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            UserAccount user = new UserAccount();
            user.Address = "";
            user.Email = "meisamshirazi70@gmail.com";
            user.UserName = "superadmin";
            user.FirstName = "مدیر";
            user.LastName = "ارشد";
            user.MobilePhone = "";
            user.Password = hashedPassword;
            user.Phone = "";
            user.RegisterDate = DateTime.Now;
            user.State = 1;
            user.Add();
            return View();
        }
    }
}