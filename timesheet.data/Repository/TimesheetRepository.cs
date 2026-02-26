using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.data.Repository
{
    public class TimesheetRepository(TimesheetDb _dbContext) : ITimeSheetRepository
    {
        public async Task<Timesheet> AddTimesheetAsync(Timesheet timesheet)
        {
           await _dbContext.Timesheets.AddAsync(timesheet);
            await _dbContext.SaveChangesAsync();
            return timesheet;
        }

        public async Task<IEnumerable<Timesheet>> GetTimesheetByEmployeeId(int empId)
        {
            return await _dbContext.Timesheets.Where(ts=>ts.EmployeeId==empId)
                .Include(x=>x.Task)
                .Include(x=>x.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Timesheet>> GetTimesheets()
        {
            return await _dbContext.Timesheets
                 .Include(x => x.Task)
                 .Include(x => x.Employee)
                 .ToListAsync();
        }
    }
}
