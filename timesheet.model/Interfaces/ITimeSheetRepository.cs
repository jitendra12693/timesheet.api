using System.Collections.Generic;
using System.Threading.Tasks;

namespace timesheet.model.Interfaces
{
    public interface ITimeSheetRepository
    {
        Task<IEnumerable<Timesheet>> GetTimesheets();
        Task<Timesheet> AddTimesheetAsync(Timesheet timesheet);
        Task<IEnumerable<Timesheet>> GetTimesheetByEmployeeId(int empId);
    }
}
