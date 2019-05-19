using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class UpdatePass
    {
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים לכל היותר 20")]
        public string OldPassword { get; set; }
        //TODO: regex for password, at least one capial ,one number and one small letter
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים לכל היותר 20")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים לכל היותר 20")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "הזן את אותה סיסמה")]
        public string ConfirmPassword { get; set; }
    }
}