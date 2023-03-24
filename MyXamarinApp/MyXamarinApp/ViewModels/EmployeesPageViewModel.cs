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
        private readonly IBlobStorageService _blobStorageService;
        public EmployeesPageViewModel(INavigationService navigationService, IBlobStorageService blobStorageService)
            : base(navigationService)
        {
            _blobStorageService = blobStorageService;
            Employees = new ObservableRangeCollection<EmployeeModel>();
            AddCommand = new DelegateCommand(async () => await AddEmployeeCommandHandler());
        }

        public DelegateCommand AddCommand { get; }

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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                Employees = new ObservableRangeCollection<EmployeeModel>(await _blobStorageService.GetAllEmployees());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task AddEmployeeCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(EmployeeRole))
            {
                return;
            }
            var newId = (Employees == null) ? 1 : Employees.Count + 1;
            EmployeeModel employee = new EmployeeModel
            {
                EmpId = newId,
                Name = EmployeeName,
                Role = EmployeeRole
            };
            Employees.Add(employee);
            await _blobStorageService.AddNewEmployee(Employees);
        }
    }
}
