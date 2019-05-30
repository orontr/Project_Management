using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

 
namespace ProjectManagement.Controllers
{
    public class ClientController : Controller
    {
        private bool Authorize()
        {
            User usr = (User)(Session["CurrentUser"]);
            if (Session["CurrentUser"] == null || usr.Type != "C")
                return false;
            else
                return true;
        }


        public ActionResult ShowClientPage()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View();
        }
   

    public ActionResult MessagesPage()
    {
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View();
        }
    }

    public ActionResult ReciverMessages()
    {
        if (!Authorize())
            return RedirectToAction("RedirectByUser", "Home");
        VMMessages msgs = new VMMessages();
        MessageDal msDal = new MessageDal();
        User curr = (User)Session["CurrentUser"];
        msgs.Messages = (from msg in msDal.messages
                         where msg.Receiver == curr.UserName
                         select msg).ToList<Message>();
        return View(msgs);
    }
    public ActionResult NewMessage()
    {
        if (!Authorize())
            return RedirectToAction("RedirectByUser", "Home");
        return View(new Message());
    }
    public ActionResult SendMessage(Message msg)
    {
        if (!Authorize())
            return RedirectToAction("RedirectByUser", "Home");
        User CurrentUser = (User)Session["CurrentUser"];
        UserDal usDal = new UserDal();
        if (usDal.Users.FirstOrDefault<User>(x => x.UserName == msg.Receiver) == null)
        {
            TempData["notUser"] = "לא קיים משתמש!";
            return RedirectToAction("NewMessage");
        }

        MessageDal msDal = new MessageDal();
        msg.Sender = CurrentUser.UserName;
        msg.DateAndTime = DateTime.Now;
        msDal.messages.Add(msg);
        msDal.SaveChanges();
        TempData["OK"] = "הודעה נשלחה למשתמש";
        return RedirectToAction("NewMessage");

    }

}
}