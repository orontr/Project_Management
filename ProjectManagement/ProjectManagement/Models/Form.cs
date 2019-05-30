using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Form
    {
        [Key]
        public string NameOfProject { get; set; }
        public string General { get; set; }
        public string Goals { get; set; }
        public string Problem { get; set; }
        public string Essence { get; set; }
        public string Implementaion { get; set; }
        public string NameOfUser { get; set; }

    }
}