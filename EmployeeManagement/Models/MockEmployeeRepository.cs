using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){ Id = 1, Name = "Edrisa", Email = "eturay@iom.int", Department = Dept.DOE},
                new Employee() { Id = 2, Name = "Beakal", Email = "bgobena@iom.int", Department = Dept.IT},
                new Employee() { Id = 3, Name = "Ijeoma", Email = "isamuel@iom.int", Department = Dept.DG}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id =  _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
