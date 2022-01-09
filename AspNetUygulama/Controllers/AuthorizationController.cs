using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetUygulama.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager adm = new AdminManager(new EFAdminDal());

        public ActionResult Index()
        {
            var values = adm.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            adm.AdminAdd(p);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var AdminValue = adm.GetByID(id);
            return View(AdminValue);
        }


        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            adm.AdminUpdate(p);
            return RedirectToAction("Index");
        }
    }
}