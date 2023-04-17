using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyXamarinApp.API.Data;
using MyXamarinApp.API.Models;
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

        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<int> RemoveEmployee(int employeeId)
        {
            var employee = _context.Employees.Where(x => x.EmpId.Equals(employeeId)).FirstOrDefault();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employeeId;
        }
    }
}
