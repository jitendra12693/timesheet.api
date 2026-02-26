using System.Collections.Generic;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.model;

namespace timesheet.business.Interfaces;

public interface ITimesheetService
{
    Task<IEnumerable<TimesheetDto>> GetTimesheets();
    Task<TimesheetDto> AddTimesheetAsync(TimesheetDto timesheet);
    Task<IEnumerable<TimesheetDto>> GetTimesheetByEmployeeId(int empId);
    Task<TimesheetDto> GetTimesheetById(int id);
}
