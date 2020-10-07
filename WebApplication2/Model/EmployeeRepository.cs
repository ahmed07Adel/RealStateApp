using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace WebApplication2.Model
{
  public  interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee>Getallemployee();
        Employee Add(Employee employee);
        Employee Update(Employee changemployee);
        Employee Delete(int Id);
        //Task<IEnumerable<Employee>> Search(string Location, int Capcity);
    }
}
