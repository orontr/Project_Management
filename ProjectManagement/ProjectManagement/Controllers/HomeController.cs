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
    public class HomeController : Controller
    {
        public ActionResult RedirectByUser()
        {
            if (Session["CurrentUser"] != null)
            {
                User currentUsr = (User)(Session["CurrentUser"]);
                if (currentUsr.Type=="M")
                    return RedirectToAction("ShowManagerPage", "Manager");
                else if (currentUsr.Type == "C")
                    return RedirectToAction("ShowClientPage", "Client");
                else
                    return RedirectToAction("ShowDeveloperPage", "Developer");
            }
            else
            {
                TempData["notAuthorized"] = "אין הרשאה!";
                return RedirectToAction("ShowHomePage");
            }
        }

        public ActionResult ShowHomePage()
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            UserLogin usr = new UserLogin();
            return View(usr);
        }

        public ActionResult Register()
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            VMUserRegister newUsr = new VMUserRegister();
            newUsr.NewUser = new User();
            return View(newUsr);
        }

        [HttpPost]
        public ActionResult RegisterSubmit(VMUserRegister usr)
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            usr.NewUser.Password = usr.Password;
            usr.NewUser.Email = usr.Email;
            ModelState.Clear();
            TryValidateModel(usr);
            if (ModelState.IsValid)
            {
                UserDal usrDal = new UserDal();

                User objUser = (from user in usrDal.Users
                                where user.UserName == usr.NewUser.UserName
                                select user).FirstOrDefault<User>();
                if (objUser != null)
                {
                    ViewBag.errorUserRegister = "שם המשתמש שבחרת קיים";
                    return View("Register", usr);
                }
                usr.NewUser.Type = "D";
                usrDal.Users.Add(usr.NewUser);
                usrDal.SaveChanges();
                ViewBag.registerSuccessMsg = "ההרשמה בוצעה בהצלחה!";
                return View("ShowHomePage", usr.NewUser);
            }
            else
            {
                usr.Password = "";
                return View("Register", usr);
            }
        }

        [HttpPost]
        public ActionResult Login(UserLogin usr)
        {
            if (Session["CurrentUser"] != null)
                return RedirectToAction("RedirectByUser");
            if (ModelState.IsValid)
            {
                UserDal usrDal = new UserDal();

                User objUser = (from user in usrDal.Users
                                where user.UserName == usr.UserName
                                select user).FirstOrDefault<User>();
                if (objUser == null || objUser.Password != usr.Password)
                {
                    ViewBag.errorUserLogin = "המשתמש או הסיסמה שגויים";
                    return View("ShowHomePage", usr);
                }
                objUser.Password = "";
                Session["CurrentUser"] = objUser;
                return RedirectToAction("RedirectByUser");
            }
            else
            {
                usr.Password = "";
                return View("ShowHomePage", usr);
            }
        }
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            return RedirectToAction("ShowHomePage");
        }

        public ActionResult ShowProfile()
        {
            if (Session["CurrentUser"] != null)
            {
                User currentUsr = (User)(Session["CurrentUser"]);
                if (currentUsr.Type=="M")
                    return RedirectToAction("ShowManagerProfile", "Manager");
                else if (currentUsr.Type == "C")
                    return RedirectToAction("ShowClientProfile", "Client");
                else
                    return RedirectToAction("ShowDeveloperProfile", "Developer");
            }
            else
            {
                TempData["notAuthorized"] = "אין הרשאה!";
                return RedirectToAction("ShowHomePage");
            }

        }
    }
}