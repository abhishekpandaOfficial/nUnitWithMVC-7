using DempApp.Web.Controllers;
using DempApp.Web.Data;
using DempApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;


[TestFixture]
public class EmployeeControllerTests
{
    [Test]
    public void Index_ReturnsViewWithEmployees()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var dbContext = new ApplicationDbContext(options))
        {
            dbContext.Database.EnsureCreated();
            // Insert sample data into the in-memory database
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Address="test Address", Designation="Test Designation", recordCreatedOn=DateTime.UtcNow },
                new Employee { Id = 2, Name = "Jane Smith",Address="test Address2", Designation="Test Designation2", recordCreatedOn=DateTime.UtcNow  }
            };
            dbContext.Employees.AddRange(employees);
            dbContext.SaveChanges();

            var controller = new EmployeeController(dbContext);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Employee>>(result.Model);

            var model = result.Model as IEnumerable<Employee>;
            Assert.AreEqual(2, model.Count());
        }
    }


    [Test]
    public void Create_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDB")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Database.EnsureCreated();

                var controller = new EmployeeController(dbContext);
                var emp = new Employee { Name = "John Doe" , Address="TestAddress3", Designation="TestDesignation3", recordCreatedOn=DateTime.UtcNow};

            // Mock TempData
            var tempDataMock = new Mock<ITempDataDictionary>();
            controller.TempData = tempDataMock.Object;
            // Act
            var result = controller.Create(emp) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("Index", result?.ActionName, "The action name should be 'Index'.");
            Assert.IsNull(result?.ControllerName, "The controller name should be null.");
            // Add more assertions if necessary
        }
    }

    [Test]
    public void Edit_WithValidModel_RedirectsToIndex()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDB")
            .Options;

        using (var dbContext = new ApplicationDbContext(options))
        {
            dbContext.Database.EnsureCreated();

            var controller = new EmployeeController(dbContext);
            var emp = new Employee { Name = "John Doe", Address = "TestAddress3", Designation = "TestDesignation3", recordCreatedOn = DateTime.UtcNow };
            dbContext.Employees.Add(emp);
            dbContext.SaveChanges();
            // Mock TempData
            var tempDataMock = new Mock<ITempDataDictionary>();
            controller.TempData = tempDataMock.Object;
            // Act
            var result = controller.Edit(emp) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }

    [Test]
    public void DeleteEmp_WithValidId_RedirectsToIndex()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDB")
            .Options;

        using (var dbContext = new ApplicationDbContext(options))
        {
            dbContext.Database.EnsureCreated();

            var controller = new EmployeeController(dbContext);
            var emp = new Employee { Id=1, Name = "John Doe", Address = "TestAddress3", Designation = "TestDesignation3", recordCreatedOn = DateTime.UtcNow };
            dbContext.Employees.Add(emp);
            dbContext.SaveChanges();
            // Mock TempData
            var tempDataMock = new Mock<ITempDataDictionary>();
            controller.TempData = tempDataMock.Object;
            // Act
            var result = controller.DeleteEmp(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }

    [Test]
    public void DeleteEmp_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDB")
            .Options;

        using (var dbContext = new ApplicationDbContext(options))
        {
            dbContext.Database.EnsureCreated();

            var controller = new EmployeeController(dbContext);
            // Mock TempData
            var tempDataMock = new Mock<ITempDataDictionary>();
            controller.TempData = tempDataMock.Object;
            // Act
            var result = controller.DeleteEmp(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
