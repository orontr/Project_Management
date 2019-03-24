using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Message
    {
        public DateTime DateOfMessage { get; set; }
        public int NumberOfMessage { get; set; }
        public String message { get; set; }
        public String Subject { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

    }
}