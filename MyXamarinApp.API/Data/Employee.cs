using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyXamarinApp.API.Data
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
