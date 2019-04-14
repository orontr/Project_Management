using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.ViewModel
{
    public class VMYourCourses
    {
        public List<Courses> courses { get; set; }
        public Courses course { get; set; }
    }
}