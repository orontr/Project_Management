using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class UserLogin
    {
        [Key]
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Username can contain only numbers and letters!")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string UserName { get; set; }
        //TODO: regex for password, at least one capial ,one number and one small letter
        [Required(ErrorMessage = "Requierd!")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "Must to contain at least capital letter, small letter and number")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "min 8 max 20 characters")]
        public string Password { get; set; }
    }
}