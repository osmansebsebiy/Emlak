using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmlakProject.Models
{
    public class MessageModel
    {
        public int msgId { get; set; }
        [DisplayName("Title")]
        [Required]
        public string msgTitle { get; set; }
        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string msgContent { get; set; }
        public int vendorId { get; set; }
        public int customerId { get; set; }
        [DisplayName("To")]
        public string vendorName { get; set; }
        [DisplayName("From")]
        public string customerName { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string msgType { get; set; }
        [DisplayName("Phone")]
        public string vendorMobile { get; set; }
        [DisplayName("Email")]
        public string vendorEmail { get; set; }
    }
}