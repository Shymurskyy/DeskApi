using DeskApi.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace DeskApi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetEmployees()
        {
            var employees = _EmployeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            var createdEmployee = _EmployeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
           

            _EmployeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}

