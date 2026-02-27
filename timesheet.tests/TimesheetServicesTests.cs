using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using timesheet.business.Services;
using timesheet.business.Dtos;
using timesheet.model.Interfaces;
using timesheet.model;
using System.Collections.Generic;
using System.Linq;

namespace timesheet.tests
{
    public class TimesheetServicesTests
    {
        [Fact]
        public async Task AddTimesheetAsync_ValidTimesheet_ReturnsDtoWithId()
        {
            var mockRepo = new Mock<ITimeSheetRepository>();
            var mockBase = new Mock<IBaseRepository<Timesheet>>();

           mockRepo.Setup(r => r.AddTimesheetAsync(It.IsAny<Timesheet>()))
              .ReturnsAsync((Timesheet t) => { t.Id = 1; return t; });

           var service = new TimesheetServices(mockRepo.Object, mockBase.Object);

           var dto = new TimesheetDto
          {
              EmployeeId = 1,
              TaskId = 1,
              StartTime = DateTime.UtcNow,
              EndTime = DateTime.UtcNow.AddHours(1),
              Remarks = "test"
          };

           var result = await service.AddTimesheetAsync(dto);

           Assert.NotNull(result);
          Assert.Equal(1, result.Id);
          Assert.Equal(dto.EmployeeId, result.EmployeeId);
      }

       [Fact]
      public async Task AddTimesheetAsync_StartTimeAfterEnd_ThrowsException()
      {
          var mockRepo = new Mock<ITimeSheetRepository>();
          var mockBase = new Mock<IBaseRepository<Timesheet>>();
          var service = new TimesheetServices(mockRepo.Object, mockBase.Object);

           var dto = new TimesheetDto
          {
              EmployeeId = 1,
              TaskId = 1,
              StartTime = DateTime.UtcNow.AddHours(2),
              EndTime = DateTime.UtcNow.AddHours(1),
          };

           await Assert.ThrowsAsync<Exception>(() => service.AddTimesheetAsync(dto));
      }

       [Fact]
      public async Task GetTimesheetById_ReturnsDto()
      {
          var mockRepo = new Mock<ITimeSheetRepository>();
          var mockBase = new Mock<IBaseRepository<Timesheet>>();

           var timesheet = new Timesheet { Id = 2, EmployeeId = 3, TaskId = 4, Remarks = "r", StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1), Employee=new Employee{Id=3,Name="E",Code="C"}, Task=new TaskMaster{Id=4,Name="T"} };
          mockBase.Setup(b => b.GetByIdAsync(2)).ReturnsAsync(timesheet);

           var service = new TimesheetServices(mockRepo.Object, mockBase.Object);

           var result = await service.GetTimesheetById(2);

           Assert.Equal(2, result.Id);
          Assert.Equal("E", result.EmployeeName);
          Assert.Equal("T", result.TaskName);
      }

       [Fact]
      public async Task GetTimesheets_ReturnsList()
      {
          var mockRepo = new Mock<ITimeSheetRepository>();
          var mockBase = new Mock<IBaseRepository<Timesheet>>();

           var list = new List<Timesheet> { new Timesheet { Id = 5, Employee=new Employee{Id=1,Name="E1",Code="C1"}, Task=new TaskMaster{Id=2,Name="T1"}, StartTime=DateTime.UtcNow, EndTime=DateTime.UtcNow.AddHours(1)} };
          mockRepo.Setup(r => r.GetTimesheets()).ReturnsAsync(list);

           var service = new TimesheetServices(mockRepo.Object, mockBase.Object);
          var result = await service.GetTimesheets();
          Assert.Single(result);
          Assert.Equal("E1", result.First().EmployeeName);
      }
  }
}
