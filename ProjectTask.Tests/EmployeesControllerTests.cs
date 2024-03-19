using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using ProjectsTask.Controllers;
using ProjectsTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsTask.Tests
{
    public class EmployeesControllerTests
    {
        private EmployeesController _controller;
        private ProjectManagmentContext _context;

        [Fact]
        public async Task GetEmployee_ReturnsEmployee()
        {
            var options = new DbContextOptionsBuilder<ProjectManagmentContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new ProjectManagmentContext(options))
            {
                var controller = new EmployeesController(context);

                var employee = new Employee { Id = 1, FirstName = "Test", LastName = "Employee", MiddleName = "xUnit", Email = "test@email.ru"};
                context.Employees.Add(employee);
                context.SaveChanges();

                var result = controller.GetEmployee(1);

                Assert.IsType<Employee>(result.Result.Value);
                Assert.Equal(employee.FirstName, result.Result.Value.FirstName);
            }
        }


        //[Fact]
        //public async Task GetAllEmployees_ReturnsOkResult()
        //{
        //    var options = new DbContextOptionsBuilder<ProjectManagmentContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDB")
        //        .Options;
        //    _context = new ProjectManagmentContext(options);
        //    _controller = new EmployeesController(_context);

        //    var result = await _controller.GetAllEmployees();

        //    Assert.IsType<OkObjectResult>(result.Result);
        //}
    }
}
