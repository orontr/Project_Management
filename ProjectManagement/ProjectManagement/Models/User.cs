using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class User:UserLogin
    {
        public int Grp { get; set; }
        [RegularExpression("[C|D|M]$", ErrorMessage = "")]
        [StringLength(1, ErrorMessage = "")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression(@" ^ ([\w\.\-] +)@([\w\-] +)((\.(\w){2, 3})+)$", ErrorMessage = "Please enter a valid email address")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name can contain only letters!")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name can contain only letters!")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string LastName { get; set; }
    }
}