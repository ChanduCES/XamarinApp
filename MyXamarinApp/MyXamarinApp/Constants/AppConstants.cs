using System;
using System.Collections.Generic;
using System.Text;

namespace MyXamarinApp.Constants
{
    public static class AppConstants
    {
        public const string Employee = nameof(Employee);

        //API Endpoints
        //Should be refered from the App.config file of platform project
        public const string BaseUrl = "https://192.168.1.101:45460/api/";
        public const string GetEmployees = "Employee/GetAllEmployees";
        public const string AddEmployee = "Employee/AddEmployee";
        public const string RemoveEmployee = "Employee/RemoveEmployee";

        //Image Resources
        public const string DeleteImage = "resource://MyXamarinApp.Resources.delete.svg";
    }
}
