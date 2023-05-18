using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees([FromQuery] EmployeeQueryParameters parameters)
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees(parameters);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployeeById(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> AddEmployee([FromBody] EmployeeModel employee)
        {
            try
            {
                var newEmployee = await _employeeRepository.AddEmployee(employee);
                if (newEmployee?.EmployeeGuid != null)
                {
                    return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.EmployeeGuid }, newEmployee);
                }
                else
                {
                    return UnprocessableEntity(employee);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployee([FromBody] EmployeeModel employee)
        {
            try
            {
                var currentEmployee = await _employeeRepository.GetEmployeeById(employee.EmployeeGuid);
                if (currentEmployee != null)
                {
                    var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
                    if (updatedEmployee != null)
                    {
                        return Ok();
                    }
                    else
                    {
                        return UnprocessableEntity(employee);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveEmployee(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee != null)
                {
                    await _employeeRepository.RemoveEmployee(employee);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
