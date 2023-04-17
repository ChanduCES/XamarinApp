using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyXamarinApp.API.Controllers;
using MyXamarinApp.API.Models;
using MyXamarinApp.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ControllerUnitTests
{
    public class EmployeeControllerTest
    {
        private readonly Fixture _fixture;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public EmployeeControllerTest()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _employeeRepositoryMock = _mockRepository.Create<IEmployeeRepository>();
        }

        private EmployeeController CreateEmployeeController()
        {
            return new EmployeeController(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldGetEmployeeList_WhenGetAllEmployees_IsCalled()
        {
            //Arrange
            var employeeController = CreateEmployeeController();
            var employees = _fixture.CreateMany<EmployeeModel>().ToList();
            _employeeRepositoryMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);

            //Act
            var result = await employeeController.GetAllEmployees();
            var employeeResult = result.Value;

            //Assert
            Assert.NotNull(employeeResult);
            Assert.Equal(employees.Count(), employeeResult.Count());
            Assert.True(employees.Equals(employeeResult));
        }

        [Fact]
        public async Task ShouldAddEmployee_WhenAddEmployee_IsCalled()
        {
            //Arrange
            var employeeController = CreateEmployeeController();
            var employee = _fixture.Create<EmployeeModel>();
            _employeeRepositoryMock.Setup(x => x.AddEmployee(employee)).ReturnsAsync(employee);

            //Act
            var result = await employeeController.AddEmployee(employee);
            var employeeResult = result.Value;

            //Assert
            Assert.NotNull(employeeResult);
            Assert.True(employee.Equals(employeeResult));
        }

        [Fact]
        public async Task ShouldRemoveEmployee_WhenRemoveEmployee_IsCalled()
        {
            //Arrange
            var employeeController = CreateEmployeeController();
            var employeeId = _fixture.Create<int>();
            _employeeRepositoryMock.Setup(x => x.RemoveEmployee(employeeId)).ReturnsAsync(employeeId);

            //Act
            var employeeResult = (OkResult)await employeeController.RemoveEmployee(employeeId);

            //Assert
            Assert.NotNull(employeeResult);
            Assert.Equal((int)HttpStatusCode.OK, employeeResult.StatusCode);
        }
    }
}
