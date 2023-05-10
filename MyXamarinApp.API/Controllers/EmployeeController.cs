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

        //[HttpGet]
        //public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees()
        //{
        //    try
        //    {
        //        var employees = await _employeeRepository.GetAllEmployees();
        //        return Ok(employees);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees([FromQuery] string searchString)
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees(searchString);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployeeById(Guid id)
        {
            try
            {
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
                if (await _employeeRepository.GetEmployeeById(newEmployee.EmployeeGuid) != null)
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
                        return NoContent();
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
                if ((await _employeeRepository.GetEmployeeById(id)) != null)
                {
                    await _employeeRepository.RemoveEmployee(id);
                    return Ok();
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
