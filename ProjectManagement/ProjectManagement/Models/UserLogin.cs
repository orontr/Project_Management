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
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "שם משתמש מכיל אותיות באנגלית ומספרים בלבד")]
        [StringLength(20, ErrorMessage = "מקסימום 20 תווים")]
        public string UserName { get; set; }
        //TODO: regex for password, at least one capial ,one number and one small letter
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "סיסמה מכילה אותיות באנגלית ומספרים בלבד")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים , מקסימום 20 תווים")]
        public string Password { get; set; }
    }
}