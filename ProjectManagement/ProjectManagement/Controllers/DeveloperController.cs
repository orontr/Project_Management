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


    }
}