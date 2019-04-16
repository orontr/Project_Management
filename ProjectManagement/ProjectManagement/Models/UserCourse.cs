using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class UserCourse
    {
        [Key, Column(Order = 0)]
        public int courseNumber { get; set; }
        [Key, Column(Order = 1)]
        public string userName { get; set; }
    }
}