﻿using AutoMapper;
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
        public async Task<List<EmployeeModel>> GetAllEmployees(string searchString, DateTime initialDate, DateTime finalDate, bool status, int currentPage, int pageSize)
        {
            List<Employee> employees = await _context.Employees.Where(x =>
                                                                    x.IsActive.Equals(status) &&
                                                                    (string.IsNullOrWhiteSpace(searchString) ||
                                                                    x.Name.Contains(searchString) ||
                                                                    x.Role.Contains(searchString) ||
                                                                    x.Salary.ToString().Contains(searchString)) &&
                                                                    (initialDate == default || x.JoiningDate >= initialDate) &&
                                                                    (finalDate == default || x.JoiningDate <= finalDate)).ToListAsync();

            employees = employees.Skip((currentPage - 1) * pageSize).Take(pageSize)?.ToList();
            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        /// <summary>
        /// Fetches the employee model for the given Id.
        /// </summary>
        /// <param name="id">Id for the employee</param>
        /// <returns>Employee model with the given Id.</returns>
        public async Task<EmployeeModel> GetEmployeeById(Guid id)
        {
            Employee employee = await _context.Employees.AsNoTracking().Where(x => x.EmployeeGuid.Equals(id)).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Add the new employee to the Employee table.
        /// </summary>
        /// <param name="employeeModel">Employee to be added.</param>
        /// <returns>Employee model of the new Employee.</returns>
        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            Employee employee = _mapper.Map<Employee>(employeeModel);
            var newEmployee = _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeModel>(newEmployee.Entity);

        }

        /// <summary>
        /// Updates the employee details to the Employee table.
        /// </summary>
        /// <param name="employeeModel">Employee to be added.</param>
        /// <returns>Employee model of the new Employee.</returns>
        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employeeModel)
        {
            Employee employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Removes the employee from the Employee table.
        /// </summary>
        /// <param name="employeeId">Employee ID of the employee to be removed.</param>
        /// <returns>True if employee is removed.</returns>
        public async Task<bool> RemoveEmployee(EmployeeModel employeeModel)
        {
            Employee employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
