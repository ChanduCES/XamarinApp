using System;
using System.Collections.Generic;
using System.Text;

namespace MyXamarinApp.Constants
{
    public static class AppConstants
    {
        public const string Employee = nameof(Employee);

        //Should be refered from the App.config file of platform project
        public const string BaseUrl = "https://localhost:45456/api/";
        public const string GetEmployees = "Employee";
    }
}
