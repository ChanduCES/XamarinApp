using MyXamarinApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyXamarinApp.API.Helper
{
    public static class MyXamarinAppHelper
    {
        public static List<Employee> ToPagedList(List<Employee> employees, int currentPage, int pageSize)
        {
            return employees.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
