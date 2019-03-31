using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class UserUpdate
    {
        [StringLength(150, ErrorMessage = "מקסימום 150 תווים")]
        public String Email { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name can contain only letters!")]
        [StringLength(50, ErrorMessage = "מקסימום 50 תווים")]
        public string FirstName { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name can contain only letters!")]
        [StringLength(20, ErrorMessage = "מקסימום 20 תווים")]
        public string LastName { get; set; }
    }
}