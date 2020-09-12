using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Model
{
    public class SQLemployeeresporitory : IEmployeeRepository
    {
        private readonly Appdbcontext context;
        public SQLemployeeresporitory(Appdbcontext context)
        {
            this.context = context;

        }


        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = context.Employees.FirstOrDefault(p => p.Id == Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();

            }

            return employee;

        }

        public IEnumerable<Employee> Getallemployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public IEnumerable<Employee> Search(string SearchTerm)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return context.Employees.ToList();
            }
            else
            {
                return context.Employees.Where(p => p.Location.Contains(SearchTerm));
            }
        }

        public Employee Update(Employee changemployee)
        {
            var employee = context.Employees.Attach(changemployee);
            employee.State = EntityState.Modified;
            context.SaveChanges();
            return changemployee;
        }
      
    }
}
