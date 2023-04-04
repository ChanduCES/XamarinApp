using System.ComponentModel.DataAnnotations;

namespace MyXamarinApp.API.Data
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
