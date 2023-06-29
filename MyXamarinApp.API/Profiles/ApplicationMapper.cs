using AutoMapper;
using MyXamarinApp.API.Data;
using MyXamarinApp.API.Models;
using System;
using System.Globalization;

namespace MyXamarinApp.API.Profiles
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<EmployeeModel, Employee>().ForMember(x => x.Salary, x => x.MapFrom(src => GetSalaryAsInt(src)));
            CreateMap<Employee, EmployeeModel>().ForMember(x => x.Salary, x => x.MapFrom(src => GetSalaryAsString(src)));
        }

        private string GetSalaryAsString(Employee employee)
        {
            return employee.Salary.ToString("N0");
        }

        private double GetSalaryAsInt(EmployeeModel employee)
        {
            return double.Parse(employee.Salary, NumberStyles.AllowThousands);
        }
    }
}
