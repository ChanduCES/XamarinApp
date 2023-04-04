using AutoMapper;
using MyXamarinApp.API.Data;
using MyXamarinApp.API.Models;

namespace MyXamarinApp.API.Profiles
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<EmployeeModel, Employee>().ReverseMap();
        }
    }
}
