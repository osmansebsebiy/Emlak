using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmlakProject.Models
{
    public class UserModel
    {
        public int userId { get; set; }
        [DisplayName("Full Name")]
        [Required]
        public string userFullName { get; set; }
        [DisplayName("Email")]
        [Required]
        public string userEmail { get; set; }
        [DisplayName("Password")]
        [Required]
        public string userPassword { get; set; }
        [DisplayName("Phone")]
        [Required]
        public string userPhone { get; set; }
    }
}