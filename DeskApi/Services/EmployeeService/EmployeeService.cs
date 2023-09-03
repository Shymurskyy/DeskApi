using DeskApi.Models;

namespace DeskApi.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.FirstOrDefault(employee => employee.EmployeeId == employeeId);
        }

        public Employee CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var employeeToRemove = GetEmployeeById(employeeId);
            if (employeeToRemove == null)
            {
                return false;
            }

            _context.Employees.Remove(employeeToRemove);
            _context.SaveChanges();
            return true;
        }
    }
 }
