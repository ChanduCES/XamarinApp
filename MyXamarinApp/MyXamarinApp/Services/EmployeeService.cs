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

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var employee = (_mapper.Map<EmployeeRequestDTO>(employeeModel));
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
