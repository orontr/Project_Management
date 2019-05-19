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

        public ActionResult AddCourse()
        {
            User CurrentUser = (User)Session["CurrentUser"];
            VMYourCourses obj = new VMYourCourses();
            obj.courses = (from c in (new CoursesDal().courses)
                           where c.userName == CurrentUser.UserName
                           select c).ToList<Courses>();
            return View(obj);
        }
        public ActionResult AddCourseSub(VMYourCourses c)
        {
            if(!Authorize())
                return RedirectToAction("RedirectByUser", "Home");
            User currentUser = (User)Session["CurrentUser"];
            c.course.userName = currentUser.UserName;
            if (ModelState.IsValid)
            {
                CoursesDal courseDal = new CoursesDal();
                courseDal.courses.Add(c.course);
                courseDal.SaveChanges();
                TempData["courseSuccessMsg"] = "קורס התווסף בהצלחה!";
            }
            return RedirectToAction("AddCourse");
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