using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Users
    {
        public int Team { get; set; }
        public string Type { get; set; }
        public String Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public long Password { get; set; }

    }
}