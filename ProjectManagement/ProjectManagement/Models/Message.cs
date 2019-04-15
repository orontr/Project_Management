using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Message
    {
        public DateTime DateAndTime { get; set; }
        [Key]
        public int NumOfMessage { get; set; }
  
        public String TextMessage { get; set; }
      
        public string Sender { get; set; }
       
        public string Receiver { get; set; }

    }
}