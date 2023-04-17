using System.ComponentModel.DataAnnotations;

namespace MyXamarinApp.API.Models
{
    public class EmployeeModel
    {
        /// <summary>
        /// Employee ID of the Employee.
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// Full name of the Employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role of the Employee.
        /// </summary>
        public string Role { get; set; }
    }
}
