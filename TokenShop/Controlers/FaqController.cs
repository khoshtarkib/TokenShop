using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TokenShop.Models;

namespace TokenShop.Controlers
{
    public class FaqController : BaseController
    {
        // GET: Faq
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("faq");
            Dictionary<string, string> paramter = new Dictionary<string, string>();
            paramter.Add("type", "2");
            paramter.Add("status", "1");
            List<Post> posts = new Post().GetAll(paramter);
            return View(posts);
        }
    }
}