using TokenShop.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TokenShop.Controlers
{
    public class PostController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            ViewBag.Title = TokenShop.Common.LanguageManagment.Translate("news");
            List<Post> model = new Post().GetAll();
            return View(model);
        }
        public ActionResult Detail(long Id)
        {
            Post pt= new Post().GetOne();
            pt.Id =Id;
            Post model = pt.GetOne();
            ViewBag.Title = model.Subject;
            return View(model);
        }
    }
}