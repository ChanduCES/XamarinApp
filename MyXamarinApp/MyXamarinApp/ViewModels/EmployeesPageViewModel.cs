using MyXamarinApp.Models;
using MyXamarinApp.Services.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MyXamarinApp.ViewModels
{
    public class EmployeesPageViewModel : ViewModelBase
    {
        private ObservableRangeCollection<EmployeeModel> _employees;
        private string _employeeName;
        private string _employeeRole;
        private readonly IEmployeeService _employeeService;

        public EmployeesPageViewModel(INavigationService navigationService, IEmployeeService employeeService)
            : base(navigationService)
        {
            _employeeService = employeeService;
            Employees = new ObservableRangeCollection<EmployeeModel>();
            AddEmployeeCommand = new DelegateCommand(async () => await AddEmployeeCommandHandler());
            DeleteEmployeeCommand = new DelegateCommand<EmployeeModel>(async (emp) => await DeleteEmployeeCommandHandler(emp));
        }

        /// <summary>
        /// Command called when the Add button is clicked.
        /// </summary>
        public DelegateCommand AddEmployeeCommand { get; }

        /// <summary>
        /// Command called when the Delete button is clicked.
        /// </summary>
        public DelegateCommand<EmployeeModel> DeleteEmployeeCommand { get; }

        /// <summary>
        /// List of Employees to be displayed.
        /// </summary>
        public ObservableRangeCollection<EmployeeModel> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                SetProperty(ref _employees, value);
            }
        }

        /// <summary>
        /// Name of the new employee to be added.
        /// </summary>
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                SetProperty(ref _employeeName, value);
            }
        }


        /// <summary>
        /// Role of the new employee to be added.
        /// </summary>
        public string EmployeeRole
        {
            get
            {
                return _employeeRole;
            }
            set
            {
                SetProperty(ref _employeeRole, value);
            }
        }

        /// <summary>
        /// Called when the app navigates to the Employee Page.
        /// </summary>
        /// <param name="parameters">Navigation parameters.</param>
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                Employees = new ObservableRangeCollection<EmployeeModel>(await _employeeService.GetAllEmployees());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Add the new employee to list by calling the AddEmployee service.
        /// </summary>
        /// <returns></returns>
        private async Task AddEmployeeCommandHandler()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeRole))
                {
                    return;
                }
                EmployeeModel employee = new EmployeeModel
                {
                    Name = EmployeeName,
                    Role = EmployeeRole
                };
                await _employeeService.AddEmployee(employee);
                Employees.ReplaceRange(await _employeeService.GetAllEmployees());
                EmployeeName = EmployeeRole = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected employee by calling the RemoveEmployee service.
        /// </summary>
        /// <param name="employee">Employee to be removed.</param>
        /// <returns></returns>
        private async Task DeleteEmployeeCommandHandler(EmployeeModel employee)
        {
            try
            {
                await _employeeService.RemoveEmployee(employee.EmpId);
                Employees.ReplaceRange(await _employeeService.GetAllEmployees());
                EmployeeName = EmployeeRole = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
