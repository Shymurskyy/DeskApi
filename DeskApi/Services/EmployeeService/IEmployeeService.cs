using DeskApi.Models;

namespace DeskApi.Services.EmployeeService
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        Employee CreateEmployee(Employee employee);
        bool DeleteEmployee(int employeeId);
    }
}
