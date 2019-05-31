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
    public class ManagerController : Controller
    {
        private bool Authorize()
        {
            User usr = (User)(Session["CurrentUser"]);
            if (Session["CurrentUser"] == null || usr.Type != "M")
                return false;
            else
                return true;
        }

        public ActionResult ShowManagerPage()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View();
        }
        public ActionResult ShowManagerProfile()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View((User)Session["CurrentUser"]);
        }
        [HttpPost]
        public ActionResult UpdateManagerProfileSubmit(UserUpdate usr)
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
                return View("ShowManagerProfile");
            }
            TempData["notUpdate"] = "לא בוצעו שינוים!";
            return RedirectToAction("ShowManagerProfile");

        }

        public ActionResult UpdatePassword()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View(new UpdatePass());
        }
        [HttpPost]
        public ActionResult UpdateManagerPassSubmit(UpdatePass pass)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser");
            User CurrentUser = (User)Session["CurrentUser"];
            TryValidateModel(pass);
            if (ModelState.IsValid && pass.OldPassword == CurrentUser.Password)
            {

                UserDal usrDal = new UserDal();
                User updateUser = usrDal.Users.FirstOrDefault(x => x.UserName == CurrentUser.UserName);
                updateUser.Password = pass.NewPassword;
                usrDal.SaveChanges();
                TempData["Update"] = "הסיסמה שונתה בהצלחה";
                return RedirectToAction("ShowManagerProfile");
            }
            TempData["notUpdate"] = "לא בוצע שינוי!";
            return RedirectToAction("ShowManagerProfile");

        }

        public ActionResult ShowGroups()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            User cur = (User)Session["CurrentUser"];
            GroupsDal grpdal = new GroupsDal();
            VMGroup vmgrp = new VMGroup();
            vmgrp.groups = grpdal.groups.ToList<Groups>();
            return View(vmgrp);
        }

        public ActionResult AddGroup()
        {
            if (!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            return View(new Groups());
        }

        [HttpPost]
        public ActionResult AddGroupSubmit(Groups pass)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            User CurrentUser = (User)Session["CurrentUser"];
            UserDal usDal = new UserDal();
            if (pass.Developer1 != null && (usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer1) == null || usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer1).Type != "D"))
            {
                TempData["notUser"] = "לא קיים מפתח1!";
                return RedirectToAction("AddGroup");
            }
            if (pass.Developer2 != null && (usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer2) == null || usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer2).Type != "D"))
            {
                TempData["notUser"] = "לא קיים מפתח2!";
                return RedirectToAction("AddGroup");
            }
            if (pass.Developer3 != null && (usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer3) == null || usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Developer3).Type != "D"))
            {
                TempData["notUser"] = "לא קיים מפתח3!";
                return RedirectToAction("AddGroup");
            }
            if (pass.Client == null || (usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Client) == null || usDal.Users.FirstOrDefault<User>(x => x.UserName == pass.Client).Type != "C"))
            {
                TempData["notUser"] = " חובה לא קיים לקוח!";
                return RedirectToAction("AddGroup");
            }
            pass.Manager = CurrentUser.UserName;
            GroupsDal grp = new GroupsDal();
            grp.groups.Add(pass);
            grp.SaveChanges();
            return RedirectToAction("ShowGroups");
        }
        public ActionResult AddClient()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            return View(new User());
        }


        [HttpPost]
        public ActionResult RegisterSubmit(User pass)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            pass.Type = "C";
            TryValidateModel(pass);
            if (ModelState.IsValid)
            {
                UserDal usdal = new UserDal();
                if (usdal.Users.FirstOrDefault(x => x.UserName == pass.UserName) == null)
                {
                    usdal.Users.Add(pass);
                    usdal.SaveChanges();
                }
                else
                {
                    TempData["notUser"] = "משתמש קיים";
                    return View("AddClient");
                }

            }
            else return View("AddClient");
            TempData["OK"] = "לקוח התווסף בהצלחה ";
            return View("ShowManagerPage");
        }




        public ActionResult ChooseForm()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            User usr = (User)Session["CurrentUser"];
            FormDal frmdal = new FormDal();
            List<Form> formss= frmdal.Forms.ToList<Form>();
            VMForms fff = new VMForms { forms = formss };
            return View(fff);
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
        [HttpPost]
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





        public ActionResult Forum()
        {
            VMForum vmForums = new VMForum();
            User current = (User)Session["CurrentUser"];
            UserCourseDal UCdal = new UserCourseDal();
            List<int> courses = (from userCourse in UCdal.courses
                                 where current.UserName == userCourse.userName
                                 select userCourse.courseNumber).ToList<int>();
            vmForums.forums = (from c in (new ForumsDal().forums)
                               where courses.Contains(c.CourseNumber)
                               select c).ToList<Forum>();
            return View(vmForums);
        }
        public ActionResult ForumSub()
        {

            return View();
        }


    }
}