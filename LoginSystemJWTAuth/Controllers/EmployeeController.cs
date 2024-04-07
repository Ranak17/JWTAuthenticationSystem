using LoginSystemJWTAuth.Models;
using LoginSystemJWTAuth.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystemJWTAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IList<Employee> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Employee AddEmployee(Employee employee)
        {
            return _employeeService.AddEmployee(employee);
        }
    }
}
