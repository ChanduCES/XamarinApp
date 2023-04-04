using System.ComponentModel.DataAnnotations;

namespace MyXamarinApp.API.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
