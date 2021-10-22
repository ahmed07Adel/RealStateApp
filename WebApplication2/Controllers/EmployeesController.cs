using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.viewmodels;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRespository;
        public EmployeesController(IEmployeeRepository employeeRespository)
        {
            _employeeRespository = employeeRespository;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeRespository.Getallemployee().ToList());
        }
        public IActionResult Get(int? id)

        {
            Homedetailsviewmodel homedetailsviewmodel = new Homedetailsviewmodel()
            {

                Employee = _employeeRespository.GetEmployee(id ?? 1)

              
            };



            return Ok(homedetailsviewmodel);
        }
       
    }
}