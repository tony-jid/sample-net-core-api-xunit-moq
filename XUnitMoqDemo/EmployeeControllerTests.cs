using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleEFCoreXUnitMoq.Controllers;
using SampleEFCoreXUnitMoq.Data.Contexts.Master;
using SampleEFCoreXUnitMoq.Models;
using SampleEFCoreXUnitMoq.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace XUnitMoqDemo
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepo> _mockEmpRepo;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _mockEmpRepo = new Mock<IEmployeeRepo>();
            _employeeController = new EmployeeController(_mockEmpRepo.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnExactNumbersOfEmployees()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(DataSeeder.MockDataEmployees());

            // Act
            var result = _employeeController.GetAll().Result;

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.NotNull(employees);
            Assert.Equal(3, employees.Count());
        }
    }
}
