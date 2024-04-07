using LoginSystemJWTAuth.Models;

namespace LoginSystemJWTAuth.ServiceContracts
{
    public interface IEmployeeService
    {
        public IList<Employee> GetEmployees();
        public Employee AddEmployee(Employee employee);
    }
}
