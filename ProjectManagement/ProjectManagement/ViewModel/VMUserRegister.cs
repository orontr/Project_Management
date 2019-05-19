﻿using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.ViewModel
{
    public class VMUserRegister
    {
        //TODO: regex for password, at least one capial ,one number and one small letter
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים לכל היותר 20")]
        public string Password { get; set; }
        //TODO: regex for password, at least one capial ,one number and one small letter
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "לפחות 8 תווים לכל היותר 20")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "הזן את אותה סיסמה")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "שדה חובה")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])", ErrorMessage = "הכנס מייל חוקי")]
        [StringLength(150, ErrorMessage = "מקסימום 150 תווים")]
        public String Email { get; set; }
        public User NewUser { get; set; }
    }

}