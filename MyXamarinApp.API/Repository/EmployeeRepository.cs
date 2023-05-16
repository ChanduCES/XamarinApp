using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyXamarinApp.API.Data;
using MyXamarinApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches the list of employees from the Employees table.
        /// </summary>
        /// <returns>List of employees.</returns>
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        public async Task<EmployeeModel> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees.AsNoTracking().Where(x => x.EmployeeGuid.Equals(id)).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Add the new employee to the Employee table.
        /// </summary>
        /// <param name="employeeModel">Employee to be added.</param>
        /// <returns>Employee model of the new Employee.</returns>
        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Updates the employee details to the Employee table.
        /// </summary>
        /// <param name="employeeModel">Employee to be added.</param>
        /// <returns>Employee model of the new Employee.</returns>
        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Removes the employee from the Employee table.
        /// </summary>
        /// <param name="employeeId">Employee ID of the employee to be removed.</param>
        /// <returns>True if employee is removed.</returns>
        public async Task<bool> RemoveEmployee(Guid employeeId)
        {
            var employee = _context.Employees.Where(x => x.EmployeeGuid.Equals(employeeId)).FirstOrDefault();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
