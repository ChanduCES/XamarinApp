using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyXamarinApp.API.Data;
using MyXamarinApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeContext _context;
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

        public async Task<int> AddEmployee(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmpId;
        }
    }
}
