using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetUygulama.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager mm = new MessageManager(new EFMessageDal());
        MessageValidator messageValidator = new MessageValidator();

        [Authorize]
        public ActionResult Inbox(string p)
        {
            var MessageList = mm.GetListInbox(p);
            return View(MessageList);
        }

        public ActionResult Sendbox(string p)
        {
            var messageList = mm.GetListSendbox(p);
            return View(messageList);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var Values = mm.GetByID(id);
            return View(Values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var Values = mm.GetByID(id);
            return View(Values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messageValidator.Validate(p);
            if(result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
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
    }
}