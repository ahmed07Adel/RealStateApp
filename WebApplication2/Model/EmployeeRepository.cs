using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace WebApplication2.Model
{
  public  interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string SearchTerm);

        Employee GetEmployee(int Id);
       IEnumerable<Employee>Getallemployee();
        Employee Add(Employee employee);
        Employee Update(Employee changemployee);
        Employee Delete(int Id);
    }
}
