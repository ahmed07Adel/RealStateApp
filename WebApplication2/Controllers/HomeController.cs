using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication2.Model;
using WebApplication2.viewmodels;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.controller
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRespository;
        private readonly Appdbcontext context;
        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(IEmployeeRepository employeeRespository, IHostingEnvironment hostingEnvironment, Appdbcontext context)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            _employeeRespository = employeeRespository;

        }


        [Route("")]
        [Route("home")]
        [Route("home/Index")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> Search(string Location, int Capcity)
        //{
        //    try
        //    {
        //        var result = await _employeeRespository.Search(Location, Capcity);
        //        if (result.Any())
        //        {

        //            return View(result.ToList());
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return View("NotFound");
        //    }

        //    return NotFound();

        //}
        //[HttpPost]
        //public string Search(string Location, int Capcity, bool notused)
        //{
        //    return "From [HttpPost]Search: filter on " + Location + Capcity;

        //}


        [Route("home/Details/{id?}")]
        public ViewResult Details(int? id)

        {
            Homedetailsviewmodel homedetailsviewmodel = new Homedetailsviewmodel()
            {

                Employee = _employeeRespository.GetEmployee(id ?? 1),

                PageTitle = "Employee Details"

            };



            return View(homedetailsviewmodel);
        }

        [Route("home/Property")]
        public async Task<ActionResult> Property(string Location, int Capcity, Employee employee)
        {
            IQueryable<Employee> query = from p in context.Employees select p;



            if (!string.IsNullOrEmpty(Location))
            {
                query = query.Where(e => e.Location.Contains(Location));
            }
            if (Capcity > 0)
            {
                query = query.Where(e => e.Capcity == employee.Capcity);
            }
            return View(await query.ToListAsync());


            //var model = _employeeRespository.Getallemployee();
            //return View(model);


        }


        [HttpGet] 
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRespository.GetEmployee(id);
            EmployeeEdit employeeEdit = new EmployeeEdit
            {
                Id = employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                Capacity = employee.Capcity,
                Location =employee.Location,
                Price = employee.Price
              
            };
            return View(employeeEdit);
        }


        [HttpPost]
        public IActionResult Edit(EmployeeEdit model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRespository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Id = model.Id;
                employee.Email = model.Email;
                employee.Capcity = model.Capacity;
                employee.Location = model.Location;
                employee.Price = model.Price;

                Employee newEmployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Id = employee.Id,
                    Photos = employee.Photos,
                    Capcity = employee.Capcity,
                    Location = employee.Location,
                    Price = employee.Price

                };
                _employeeRespository.Update(employee);
                return RedirectToAction("Property");
            }

            return View();

        }


        [AllowAnonymous]
        [Route("home/Property/Delete/{id?}")]
        public IActionResult Delete(int Id)
        {
            _employeeRespository.Delete(Id);
            return RedirectToAction("Property");
        }

        [HttpPost]
        [AllowAnonymous]     
        public IActionResult Create(EmployeeCreateViewModel model)

        {
            if (ModelState.IsValid)
            {
                string UniqueFile = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile Photo in model.Photos)
                    {
                        string UploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        UniqueFile = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                        string FilePath = Path.Combine(UploadFolder, UniqueFile);
                        Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
                    }
                } 
                Employee newEmployee = new Employee
                {
                    
                    Name = model.Name,
                    Email =model.Email,
                    Photos =UniqueFile,
                    Capcity = model.Capacity,
                    Location = model.Location,
                    Price = model.Price
                };
                _employeeRespository.Add(newEmployee); //saved to data base table
                return RedirectToAction(nameof(Property), new {id = newEmployee.Id});
            }

            return View();
        }
    }
}


