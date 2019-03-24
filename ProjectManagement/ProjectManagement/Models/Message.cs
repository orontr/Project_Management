using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Message
    {
        public DateTime DateOfMessage { get; set; }
        [Key]
        public int NumberOfMessage { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "Maximum 300 characters.")]
        public String message { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "Maximum 35 characters.")]
        public String Subject { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "Maximum 35 characters.")]
        public string Sender { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "Maximum 35 characters.")]
        public string Receiver { get; set; }

    }
}