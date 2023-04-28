using Microsoft.AspNetCore.Mvc;
using MyXamarinApp.API.Models;
using MyXamarinApp.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            if (employees != null)
            {
                return Ok(employees);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployeeById(Guid id)
        {
            var employees = await _employeeRepository.GetEmployeeById(id);
            if (employees.Any())
            {
                return Ok(employees);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> AddEmployee([FromBody] EmployeeModel employee)
        {
            var newEmployee = await _employeeRepository.AddEmployee(employee);
            if(newEmployee != null)
            {
                return CreatedAtAction(nameof(AddEmployee), newEmployee.EmployeeGuid, newEmployee);
            }
            else
            {
                return UnprocessableEntity(employee);
            }
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployee([FromBody] EmployeeModel employee)
        {
            var currentEmployee = await _employeeRepository.GetEmployeeById(employee.EmployeeGuid);
            if (currentEmployee.Any())
            {
                var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
                if(updatedEmployee != null)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveEmployee(Guid id)
        {
            if ((await _employeeRepository.GetEmployeeById(id)).Any())
            {
                await _employeeRepository.RemoveEmployee(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
