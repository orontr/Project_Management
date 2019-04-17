using System;
using System.Linq;
using System.Web.Mvc;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.ViewModel;

namespace ProjectManagement.Controllers
{
    public class DeveloperController : Controller
    {
        private bool Authorize()
        {
            User usr = (User)(Session["CurrentUser"]);
            if (Session["CurrentUser"] == null || usr.Type != "D")
                return false;
            else
                return true;
        }

        public ActionResult ShowDeveloperPage()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View();
        }

  
        public ActionResult ShowDeveloperProfile()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View((User)Session["CurrentUser"]);
        }
        [HttpPost]
        public ActionResult UpdateDeveloperProfileSubmit(UserUpdate usr)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser");
            User CurrentUser = (User)Session["CurrentUser"];
            TryValidateModel(usr);
            if (ModelState.IsValid)
            {
               
                UserDal usrDal = new UserDal();
                User updateUser = usrDal.Users.FirstOrDefault(x => x.UserName == CurrentUser.UserName);
                updateUser.FirstName = usr.FirstName;
                updateUser.LastName = usr.LastName;
                updateUser.Email = usr.Email;
                usrDal.SaveChanges();
                TempData["Update"] = "השינויים בוצעו בהצלחה";
                Session["CurrentUser"] = usrDal.Users.FirstOrDefault(x => x.UserName == CurrentUser.UserName);
                return View("ShowDeveloperProfile");
            }
            TempData["notUpdate"] = "לא בוצעו שינוים!";
            return RedirectToAction("ShowDeveloperProfile");

        }

        public ActionResult UpdatePassword()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View(new UpdatePass());
        }
        [HttpPost]
        public ActionResult UpdateDeveloperPassSubmit(UpdatePass pass)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser");
            User CurrentUser = (User)Session["CurrentUser"];
            TryValidateModel(pass);
            if (ModelState.IsValid && pass.OldPassword==CurrentUser.Password)
            {

                UserDal usrDal = new UserDal();
                User updateUser = usrDal.Users.FirstOrDefault(x => x.UserName == CurrentUser.UserName);
                updateUser.Password = pass.NewPassword;
                usrDal.SaveChanges();
                TempData["Update"] = "הסיסמה שונתה בהצלחה";
                return RedirectToAction("ShowDeveloperProfile");
            }
            TempData["notUpdate"] = "לא בוצע שינוי!";
            return RedirectToAction("ShowDeveloperProfile");

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