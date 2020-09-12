using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.viewmodels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        //public Dept? Departement { get; set; }
        public List<IFormFile> Photos { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
