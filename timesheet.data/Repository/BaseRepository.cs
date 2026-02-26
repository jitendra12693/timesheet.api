using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.data.Repository
{
    public class BaseRepository(TimesheetDb _dbContext) : IBaseRepository
    {
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<TaskMaster>> GetAllTask()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<TaskMaster>> GetFilteredTask(string searchTerm)//,int pageSize,int pageNum)
        {
            var  query = _dbContext.Tasks.AsQueryable();
            if(!string.IsNullOrEmpty(searchTerm))
                query = query.Where(t=>t.Name.Contains(searchTerm) || t.Description.Contains(searchTerm));
            return await query.ToListAsync();
        }
        public async Task<Employee> GetEmployeeByCode(string code)
        {
            return await _dbContext.Employees.Where(e=>e.Code== code).FirstOrDefaultAsync();
        }
    }
}
