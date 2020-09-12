using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Utilities;
using Xunit;

namespace WebApplication2.viewmodels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        [ValidEmailDomain(AllowDomain:"Gmail.com", ErrorMessage = "Email Must Be Gmail.com") ]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name ="Comfirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesnot match")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
