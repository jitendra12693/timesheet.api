using System.Threading.Tasks;
using Moq;
using Xunit;
using timesheet.api.controllers;
using timesheet.business.Interfaces;
using timesheet.business.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace timesheet.tests
{
    public class TimesheetControllerTests
    {
        [Fact]
        public async Task Post_Valid_ReturnsCreated()
        {
            var mockService = new Mock<ITimesheetService>();
            mockService.Setup(s => s.AddTimesheetAsync(It.IsAny<TimesheetDto>())).ReturnsAsync((TimesheetDto t) => { t.Id = 10; return t; });
            var controller = new TimesheetController(mockService.Object);
            var dto = new TimesheetDto { EmployeeId = 1, TaskId = 1, StartTime = System.DateTime.UtcNow, EndTime = System.DateTime.UtcNow.AddHours(1) };
            var result = await controller.Post(dto);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            var mockService = new Mock<ITimesheetService>();
            mockService.Setup(s => s.GetTimesheets()).ReturnsAsync(new List<TimesheetDto>());
            var controller = new TimesheetController(mockService.Object);
            var result = await controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
