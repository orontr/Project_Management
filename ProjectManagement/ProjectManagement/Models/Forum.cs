using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Forum
    {
        public DateTime DateOfMessage { get; set; }
        public int CourseNumber { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}