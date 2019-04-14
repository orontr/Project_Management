using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }
        public string userName  { get; set; }
        public string name { get; set; }
        
    }
}