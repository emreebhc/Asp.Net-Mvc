using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetUygulama.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default

        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        ContentManager cm = new ContentManager(new EFContentDal());
        public ActionResult Headings()
        {
            var HeadingList = hm.GetList();
            return View(HeadingList);
        }

        public PartialViewResult Index(int id=0)
        {
            var Contentlist = cm.GetListByHeadingID(id);
            return PartialView(Contentlist);
        }
    }
}