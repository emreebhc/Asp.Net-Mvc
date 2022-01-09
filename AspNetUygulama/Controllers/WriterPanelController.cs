using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace AspNetUygulama.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        WriterManager wm = new WriterManager(new EFWriterDal());
        WriterValidator writerValidator = new WriterValidator();

        Context c = new Context();

        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string p = (string)Session["WriterMail"];
           id = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var WriterValue = wm.GetByID(id);
            return View(WriterValue);
        }


        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult result = writerValidator.Validate(p);
            if (result.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }






        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
          var  writerID = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writerID);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
        
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            string writerMailInfo = (string)Session["WriterMail"];
            var writerID = c.Writers.Where(x => x.WriterMail == writerMailInfo).Select(y => y.WriterID).FirstOrDefault();
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerID;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }


        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
           //p.HeadingStatus = true;
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }


        public ActionResult DeleteHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            HeadingValue.HeadingStatus = false;
            hm.HeadingDelete(HeadingValue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int p=1)
        {

            var headings = hm.GetList().ToPagedList(p,4);
            return View(headings);
        }
    }
}