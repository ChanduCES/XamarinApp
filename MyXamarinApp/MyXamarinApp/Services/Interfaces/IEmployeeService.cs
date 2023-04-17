using MyXamarinApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel);
        Task<bool> RemoveEmployee(int employeeId);
    }
}
