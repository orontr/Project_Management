using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class User:UserLogin
    {
        public int Team { get; set; }
        [RegularExpression("[C|D|M]$", ErrorMessage = "")]
        [StringLength(1, ErrorMessage = "")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression(@" ^ ([\w\.\-] +)@([\w\-] +)((\.(\w){2, 3})+)$", ErrorMessage = "Please enter a valid email address")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Username can contain only numbers and letters!")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Username can contain only numbers and letters!")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string LastName { get; set; }
    }
}