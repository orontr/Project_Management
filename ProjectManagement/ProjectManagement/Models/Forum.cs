using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Forum
    {
        [Key]
        public DateTime DateOfPost { get; set; }
        public int CourseNumber { get; set; }
        public string Post { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}