using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Model
{
    public class MockEmloyee : IEmployeeRepository
    {
        private List<Employee> _employeelist;
        public MockEmloyee()
        {
            _employeelist = new List<Employee>()
            {
            new Employee(){Id = 1, Name = "ahmed", Email = "1213@gmail", Capacity = 200, Location = "helioplis", Price = 100000},
            new Employee(){ Id = 2, Name = "ali", Email = "1113@gmail", Capacity = 300, Location = "mdint nasr", Price = 300000 },
            new Employee(){ Id = 3, Name = "mhmd", Email = "1513@gmail", Capacity = 200, Location = "alex", Price = 1500000 }
            };

        }
        public IEnumerable<Employee> Search(string SearchTerm)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return _employeelist;
            }
            else
            {
                return _employeelist.Where(p => p.Location.Contains(SearchTerm));
            }
        }

            public Employee Add(Employee employee)
        {
            employee.Id = _employeelist.Max(e => e.Id) + 1;
            _employeelist.Add(employee);
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _employeelist.FirstOrDefault(p => p.Id == Id);
            if (employee != null)
            {
                _employeelist.Remove(employee);
            }

            return employee;
        }

      

        public IEnumerable<Employee> Getallemployee()
        {
            return _employeelist;
        }

        public Employee GetallEmployee(int id)
        {
            return _employeelist.FirstOrDefault(e => e.Id == id);

        }

        public Employee GetEmployee(int id)
        {
            return _employeelist.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> Getemployee()
        {
            return _employeelist;
        }

        public Employee update(Employee changemployee)
        {
            Employee employee = _employeelist.FirstOrDefault(e => e.Id == changemployee.Id);
            if (employee != null)
            {
                employee.Name = changemployee.Name;
                employee.Email = changemployee.Email;
                employee.Id = changemployee.Id;
                employee.Photos = changemployee.Photos;
                employee.Price = changemployee.Price;
                employee.Location = changemployee.Location;
                employee.Capacity = changemployee.Capacity;
            }

            return employee;
        }

        public Employee Update(Employee changemployee)
        {
            throw new NotImplementedException();
        }
    }
}
