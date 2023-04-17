using AutoMapper;
using Flurl.Http;
using Flurl.Http.Configuration;
using MyXamarinApp.Constants;
using MyXamarinApp.DTO;
using MyXamarinApp.Models;
using MyXamarinApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyXamarinApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IFlurlClient _flurlClient;
        private readonly IMapper _mapper;

        public EmployeeService(IFlurlClientFactory flurlClient, IMapper mapper)
        {
            _flurlClient = flurlClient.Get(AppConstants.BaseUrl);
            _mapper = mapper;
        }

        /// <summary>
        /// Service to call the GetAllEmployees API to fetch the list of employees.
        /// </summary>
        /// <returns>List of all employees</returns>
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            try
            {
                var response = await _flurlClient.Request(AppConstants.GetEmployees)
                    .GetJsonAsync<List<EmployeeResponseDTO>>();
                return _mapper.Map<List<EmployeeModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Service to add a new employee to the list.
        /// </summary>
        /// <param name="employeeModel">Employee to be added.</param>
        /// <returns>Employee model of the new employee.</returns>
        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var employee = _mapper.Map<EmployeeRequestDTO>(employeeModel);
                var response = await _flurlClient.Request(AppConstants.AddEmployee)
                    .PostJsonAsync(employee);
                var newEmployee = await response.GetJsonAsync<EmployeeResponseDTO>();
                return _mapper.Map<EmployeeModel>(newEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Service call to remove an employee from the list.
        /// </summary>
        /// <param name="employeeId">Employee ID of employee to be removed.</param>
        /// <returns>True if the Employee is successfully removed, false otherwise.</returns>
        public async Task<bool> RemoveEmployee(int employeeId)
        {
            try
            {
                var response = await _flurlClient.Request(AppConstants.RemoveEmployee)
                    .PostJsonAsync(employeeId);
                return response.StatusCode.Equals(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
