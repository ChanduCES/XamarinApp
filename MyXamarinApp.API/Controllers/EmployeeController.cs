using Microsoft.AspNetCore.Mvc;
using MyXamarinApp.API.Models;
using MyXamarinApp.API.Repository;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel employee)
        {
            var id = await _employeeRepository.AddEmployee(employee);
            return Ok(id);
        }
    }
}
