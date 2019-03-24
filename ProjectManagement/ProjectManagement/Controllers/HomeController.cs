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
        //public ActionResult RedirectByUser()
        //{
        //    if (Session["CurrentUser"] != null)
        //    {
        //        User currentUsr = (User)(Session["CurrentUser"]);
        //        if (currentUsr.IsManager)
        //            return RedirectToAction("ShowManagerPage", "Manager");
        //        else
        //            return RedirectToAction("ShowRoommatePage", "Roommate");
        //    }
        //    else
        //    {
        //        TempData["notAuthorized"] = "אין הרשאה!";
        //        return RedirectToAction("HomePage");
        //    }
        //}

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

    }
}