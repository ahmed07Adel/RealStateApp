using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class AppUsers : IdentityUser
    {
        public string City { get; set; }
    }
}
