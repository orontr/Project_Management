using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class GroupUsers
    {
        public int Team { get; set; }
        public List<string> Developers { get; set; }
        public string Client { get; set; }
        public string Manager { get; set; }
    }
}