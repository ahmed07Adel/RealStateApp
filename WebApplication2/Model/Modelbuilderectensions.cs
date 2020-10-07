using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Model
{
    public static class Modelbuilderectensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "sayed",
                    Email = "marko@gmail.com",
                    Price = 200000,
                    Location = "Mdint nasr",
                    Capcity = 200
                },
                new Employee
                {
                    Id = 2,
                    Name = "marko",
                    Email = "12334@gmail.com",
                    Price = 200000,
                    Location = "Mdint nasr",
                    Capcity = 200
                }
            );
        }
    }
}
