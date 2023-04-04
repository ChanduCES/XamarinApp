using Microsoft.EntityFrameworkCore;
using MyXamarinApp.API.Data;

namespace MyXamarinApp.API.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
