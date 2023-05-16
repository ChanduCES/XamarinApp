using MyXamarinApp.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> GetEmployeeById(Guid id);
        Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel employeeModel);
        Task<bool> RemoveEmployee(Guid employeeId);
    }
}
