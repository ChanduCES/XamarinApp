using MyXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyXamarinApp.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task AddNewEmployee(IEnumerable<EmployeeModel> employees);
        Task<IEnumerable<EmployeeModel>> GetAllEmployees();
    }
}
