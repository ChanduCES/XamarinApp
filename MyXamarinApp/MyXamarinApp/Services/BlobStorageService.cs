using Akavache;
using MyXamarinApp.Constants;
using MyXamarinApp.Models;
using MyXamarinApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MyXamarinApp.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobCache _blobCache;
        public BlobStorageService(IBlobCache blobCache)
        {
            _blobCache = blobCache;
        }

        public async Task AddNewEmployee(IEnumerable<EmployeeModel> employees)
        {
            await _blobCache.InsertObject(AppConstants.Employee, employees);
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            var employees = await _blobCache.GetObject<IEnumerable<EmployeeModel>>(AppConstants.Employee);
            return employees;
        }
    }
}
