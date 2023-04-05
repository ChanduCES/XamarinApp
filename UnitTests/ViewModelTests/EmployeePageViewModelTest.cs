using AutoFixture;
using FluentAssertions;
using Moq;
using MyXamarinApp.Models;
using MyXamarinApp.Services.Interfaces;
using MyXamarinApp.ViewModels;
using Prism.Navigation;
using System;
using System.Linq;
using Xunit;

namespace UnitTests.ViewModelTests
{
    public class EmployeePageViewModelTest
    {
        private readonly Fixture _fixture;
        private readonly MockRepository _mockRepository;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IEmployeeService> _employeeServiceMock;

        public EmployeePageViewModelTest()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _navigationServiceMock = _mockRepository.Create<INavigationService>();
            _employeeServiceMock = _mockRepository.Create<IEmployeeService>();
        }

        private EmployeesPageViewModel CreateViewModel()
        {
            return new EmployeesPageViewModel(_navigationServiceMock.Object, _employeeServiceMock.Object);
        }

        [Fact]
        public void ShouldNotThrowAnyException_WhenEmployeesPageViewModelConstructor_IsCalled()
        {
            Action action = () => CreateViewModel();
            action.Should().NotThrow<Exception>();
        }

        [Fact]
        public void ShouldPopulateEmployees_WhenOnNavigatedTo_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>().ToList();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);

            //Assert
            employeePageViewModel.Employees.Should().BeEquivalentTo(employees);
            _employeeServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldThrowExceptionFromGetAllEmployees_WhenOnNavigatedTo_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var exception = _fixture.Create<Exception>();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).Throws(exception);

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);

            //Assert
            employeePageViewModel.Employees.Should().BeEmpty();
            _employeeServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldAddEmployeeToList_WhenAddCommand_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>().ToList();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            employeePageViewModel.EmployeeName = _fixture.Create<string>();
            employeePageViewModel.EmployeeRole = _fixture.Create<string>();
            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);
            _employeeServiceMock.Setup(x => x.AddEmployee(It.IsAny<EmployeeModel>())).ReturnsAsync(It.IsAny<EmployeeModel>());
            employeePageViewModel.AddCommand.Execute();

            //Assert
            _employeeServiceMock.Verify(x => x.GetAllEmployees(), Times.Exactly(2));
            _employeeServiceMock.Verify(x => x.AddEmployee(It.IsAny<EmployeeModel>()), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldThrowException_WhenAddCommand_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>().ToList();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            employeePageViewModel.EmployeeName = _fixture.Create<string>();
            employeePageViewModel.EmployeeRole = _fixture.Create<string>();
            var exception = _fixture.Create<Exception>();

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);
            _employeeServiceMock.Setup(x => x.AddEmployee(It.IsAny<EmployeeModel>())).Throws(exception);
            employeePageViewModel.AddCommand.Execute();

            //Assert
            _employeeServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _employeeServiceMock.Verify(x => x.AddEmployee(It.IsAny<EmployeeModel>()), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldReturnIfNameOrRoleIsEmpty_WhenAddCommand_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>().ToList();
            _employeeServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            employeePageViewModel.EmployeeName = string.Empty;
            employeePageViewModel.EmployeeRole = string.Empty;
            var employee = _fixture.Build<EmployeeModel>().With(x => x.Name, employeePageViewModel.EmployeeName).With(x => x.Role, employeePageViewModel.EmployeeRole).Create();

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);
            employeePageViewModel.AddCommand.Execute();

            //Assert
            employeePageViewModel.Employees.Count().Should().Be(employees.Count());
            _employeeServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _employeeServiceMock.Verify(x => x.AddEmployee(employee), Times.Never);
            _mockRepository.VerifyAll();
        }
    }
}
