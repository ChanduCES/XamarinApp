using MyXamarinApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel);
        Task<int> RemoveEmployee(int employeeId);
    }
}
