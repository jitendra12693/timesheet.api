using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.business.Services;

public class TimesheetServices(ITimeSheetRepository _timeSheetRepository) : ITimesheetService
{
    public async Task<TimesheetDto> AddTimesheetAsync(TimesheetDto timesheet)
    {

        if (timesheet.StartTime < timesheet.EndTime)
            throw new System.Exception("Task start time should be greater than  end time");
        else if(timesheet.EndTime == timesheet.StartTime)
            throw new System.Exception("Task start time and end time should not same date time");

        var entity = new Timesheet
            {
                EmployeeId = timesheet.EmployeeId,
                TaskId = timesheet.TaskId,
                Remarks = timesheet.Remarks,
                StartTime = timesheet.StartTime,
                EndTime = timesheet.EndTime
            };
        var result = await _timeSheetRepository.AddTimesheetAsync(entity);
        return new TimesheetDto
        {
            EmployeeId = entity.EmployeeId,
            TaskId = entity.TaskId,
            Id = entity.Id,
            Remarks = entity.Remarks,
        };
    }

    public async Task<IEnumerable<TimesheetDto>> GetTimesheetByEmployeeId(int empId)
    {
        var result = await _timeSheetRepository.GetTimesheetByEmployeeId(empId);
        return result.Select(t=>new TimesheetDto
        {
            EmployeeId = t.EmployeeId,
            TaskId = t.TaskId,
            Id = t.Id,
            Remarks = t.Remarks,
            StartTime=t.StartTime,
            EndTime=t.EndTime,
            EmpCode = t?.Employee?.Code,
            EmployeeName = t?.Employee?.Name,
            TaskName = t?.Task?.Name,
        });
    }

    public async Task<IEnumerable<TimesheetDto>> GetTimesheets()
    {
        var result = await _timeSheetRepository.GetTimesheets();
        return result.Select(t => new TimesheetDto
        {
            EmployeeId = t.EmployeeId,
            TaskId = t.TaskId,
            Id = t.Id,
            Remarks = t.Remarks,
            StartTime = t.StartTime,
            EndTime = t.EndTime,
            EmpCode = t?.Employee?.Code,
            EmployeeName = t?.Employee?.Name,
            TaskName = t?.Task?.Name,
        });
    }
}
