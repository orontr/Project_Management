using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.ViewModel
{
    public class VMGroupUsers
    {
        public List<GroupUsers> groups { get; set; }

        public VMGroupUsers()
        {
            groups = new List<GroupUsers>(); 
        }
    }
}