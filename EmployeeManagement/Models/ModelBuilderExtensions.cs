using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Edrisa",
                   Department = Dept.DOE,
                   Email = "eturay@iom.int"
               },
               new Employee
               {
                   Id = 2,
                   Name = "Beakal",
                   Department = Dept.DOE,
                   Email = "bgobena@iom.int"
               }
            );
        }
    }
}
