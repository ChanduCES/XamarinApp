using Microsoft.AspNetCore.Mvc;
using MyXamarinApp.API.Models;
using MyXamarinApp.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> AddEmployee([FromBody] EmployeeModel employee)
        {
            var newEmployee = await _employeeRepository.AddEmployee(employee);
            return newEmployee;
        }

        [HttpPost]
        public async Task<ActionResult> RemoveEmployee([FromBody] int id)
        {
            await _employeeRepository.RemoveEmployee(id);
            return Ok();
        }
    }
}
