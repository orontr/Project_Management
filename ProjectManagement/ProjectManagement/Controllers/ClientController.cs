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


        public ActionResult ChooseForm()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            User usr = (User)Session["CurrentUser"];
            GroupsDal grpdal = new GroupsDal();
            List<string> a = (from Groups g in grpdal.groups
                              where usr.UserName == g.Client
                              select g.Developer1).ToList<string>();
            a.AddRange((from Groups g in grpdal.groups
                        where usr.UserName == g.Client
                        select g.Developer2).ToList<string>());
            a.AddRange((from Groups g in grpdal.groups
                        where usr.UserName == g.Client
                        select g.Developer2).ToList<string>());
            HashSet<string> b = new HashSet<string>();
            foreach (string c in a)
                b.Add(c);
            FormDal frmdal = new FormDal();
            List<Form> formss = (from Form f in frmdal.Forms
                                 where b.Contains(f.NameOfUser)
                                 select f).ToList<Form>();
            VMForms fff = new VMForms { forms = formss };
            return View(fff);
        }

        [HttpGet]
        public ActionResult ShowForm(string name)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            FormDal frmdal = new FormDal();
            Form form = frmdal.Forms.FirstOrDefault<Form>(x => x.NameOfProject == name);
            return View(form);
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