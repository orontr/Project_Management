using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }
    }
}