using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.ViewModel;
using Rotativa;

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
            if (ModelState.IsValid && pass.OldPassword == CurrentUser.Password)
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
        public ActionResult CreateForm()
        {


            return View(new Form());
        }
        [HttpPost]
        public ActionResult SaveSubmit(Form frm)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser");
            User usr = (User)Session["CurrentUser"];
            if (ModelState.IsValid)
            {
                frm.NameOfUser = usr.UserName;
                FormDal frmDal = new FormDal();
                frmDal.Forms.Add(frm);
                frmDal.SaveChanges();
            }

            return RedirectToAction("ShowDeveloperPage"); ;
        }
        public ActionResult ChooseForm()
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            User usr = (User)Session["CurrentUser"];
            FormDal frmdal = new FormDal();
            List<Form> formss = (from f in frmdal.Forms
                                 where usr.UserName == f.NameOfUser
                                 select f).ToList<Form>();
            VMForms fff = new VMForms { forms = formss };
            return View(fff);
        }


        [HttpGet]
        public ActionResult EditForm(string name)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser", "Home");
            FormDal frmdal = new FormDal();
            Form form = frmdal.Forms.FirstOrDefault<Form>(x => x.NameOfProject == name);
            return View(form);

        }
        [HttpPost]
        public ActionResult SaveChangeSubmit(Form form)
        {
            if (Session["CurrentUser"] == null)
                return RedirectToAction("RedirectByUser");
            User CurrentUser = (User)Session["CurrentUser"];
            TryValidateModel(form);
            if (ModelState.IsValid)
            {

                FormDal usrDal = new FormDal();
                Form f = usrDal.Forms.FirstOrDefault(x => x.NameOfProject == form.NameOfProject);
                f.General = form.General;
                f.Essence = form.Essence;
                f.Goals = form.Goals;
                f.Implementaion = form.Implementaion;
                usrDal.SaveChanges();
                TempData["Update"] = "השינויים בוצעו בהצלחה";
                return View("ShowDeveloperPage");
            }
            TempData["notUpdate"] = "לא בוצעו שינוים!";
            return RedirectToAction("ShowDeveloperPage");

        }

        public ActionResult PrintPartialViewToPdf(string name)
        {

            Form f = (new FormDal()).Forms.FirstOrDefault(x => x.NameOfProject == name);
            var report = new PartialViewAsPdf("~/Views/Developer/EditForm.cshtml", f);
            return report;


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

