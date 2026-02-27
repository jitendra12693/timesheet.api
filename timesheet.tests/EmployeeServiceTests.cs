using System.Threading.Tasks;
using Moq;
using Xunit;
using timesheet.business.Services;
using Microsoft.Extensions.Configuration;
using timesheet.model.Interfaces;
using timesheet.model;
using System.Collections.Generic;
using System.Linq;

namespace timesheet.tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async Task GetEmployees_ReturnsList()
        {
            var mockBase = new Mock<IBaseRepository<Employee>>();
            mockBase.Setup(b => b.GetAllAsync()).ReturnsAsync(new List<Employee> { new Employee { Id = 1, Name = "A", Code = "C" } });
            var mockConfig = new Mock<IConfiguration>();
            var service = new EmployeeService(mockBase.Object, mockConfig.Object);
            var result = await service.GetAllEmployee();
            Assert.Single(result);
        }
    }
}
