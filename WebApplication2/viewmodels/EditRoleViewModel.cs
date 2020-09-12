using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace WebApplication2.viewmodels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "role name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
