using LoginSystemJWTAuth.Models;
using LoginSystemJWTAuth.ServiceContracts;
using Microsoft.AspNetCore.Authorization;

namespace LoginSystemJWTAuth.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static IList<Employee> _employeeList;
        static EmployeeService()
        {
            _employeeList = new List<Employee>();
        }
        public Employee AddEmployee(Employee employee)
        {
            _employeeList.Add(employee);
            return employee;
        }

        public IList<Employee> GetEmployees()
        {
            return _employeeList;
        }
    }
}
