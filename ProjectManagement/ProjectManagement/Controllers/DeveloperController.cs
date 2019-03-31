using ProjectManagement.DAL;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                return View("ShowDeveloperProfile");
            }
            TempData["notUpdate"] = "לא בוצעו שינוים!";
            return View("ShowDeveloperProfile");

        }
    }
}