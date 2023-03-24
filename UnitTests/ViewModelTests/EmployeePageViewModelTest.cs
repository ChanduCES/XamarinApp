using AutoFixture;
using FluentAssertions;
using Moq;
using MyXamarinApp.Models;
using MyXamarinApp.Services.Interfaces;
using MyXamarinApp.ViewModels;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ViewModelTests
{
    public class EmployeePageViewModelTest
    {
        private readonly Fixture _fixture;
        private readonly MockRepository _mockRepository;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IBlobStorageService> _blobStorageServiceMock;

        public EmployeePageViewModelTest()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _navigationServiceMock = _mockRepository.Create<INavigationService>();
            _blobStorageServiceMock = _mockRepository.Create<IBlobStorageService>();
        }

        private EmployeesPageViewModel CreateViewModel()
        {
            return new EmployeesPageViewModel(_navigationServiceMock.Object,_blobStorageServiceMock.Object);
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

            var employees = _fixture.CreateMany<EmployeeModel>();
            _blobStorageServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);

            //Assert
            employeePageViewModel.Employees.Should().BeEquivalentTo(employees);
            _blobStorageServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldThrowExceptionFromGetAllEmployees_WhenOnNavigatedTo_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var exception = _fixture.Create<Exception>();
            _blobStorageServiceMock.Setup(x => x.GetAllEmployees()).Throws(exception);

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);

            //Assert
            employeePageViewModel.Employees.Should().BeEmpty();
            _blobStorageServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldAddEmployeeToList_WhenAddCommand_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>();
            _blobStorageServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            employeePageViewModel.EmployeeName = _fixture.Create<string>();
            employeePageViewModel.EmployeeRole = _fixture.Create<string>();

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);
            _blobStorageServiceMock.Setup(x => x.AddNewEmployee(employeePageViewModel.Employees)).Returns(Task.FromResult(true));
            employeePageViewModel.AddCommand.Execute();

            //Assert
            employeePageViewModel.Employees.Count().Should().BeGreaterThan(employees.Count());
            _blobStorageServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _blobStorageServiceMock.Verify(x => x.AddNewEmployee(employeePageViewModel.Employees), Times.Once);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void ShouldReturnIfNameOrRoleIsEmpty_WhenAddCommand_IsCalled()
        {
            //Arrange
            var employeePageViewModel = CreateViewModel();
            var navigationParameters = new NavigationParameters();

            var employees = _fixture.CreateMany<EmployeeModel>();
            _blobStorageServiceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);
            employeePageViewModel.EmployeeName = string.Empty;
            employeePageViewModel.EmployeeRole = string.Empty;

            //Act
            employeePageViewModel.OnNavigatedTo(navigationParameters);
            employeePageViewModel.AddCommand.Execute();

            //Assert
            employeePageViewModel.Employees.Count().Should().Be(employees.Count());
            _blobStorageServiceMock.Verify(x => x.GetAllEmployees(), Times.Once);
            _blobStorageServiceMock.Verify(x => x.AddNewEmployee(employeePageViewModel.Employees), Times.Never);
            _mockRepository.VerifyAll();
        }
    }
}
