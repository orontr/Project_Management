using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Team { get; set; }
        public string Developer1 { get; set; }
        public string Developer2 { get; set; }
        public string Developer3 { get; set; }
        public string Client { get; set; }
        public string Manager { get; set; }
    }
}