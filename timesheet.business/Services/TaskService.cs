using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.business.Services
{
    public class TaskService:ITaskService
    {
        private readonly IBaseRepository<TaskMaster> _taskRepo;
        public TaskService(IBaseRepository<TaskMaster> taskRepo)
        {
            _taskRepo = taskRepo;
        }
        public async Task<IEnumerable<TaskDto>> GetAllTask()
        {
            var result = await _taskRepo.FindAsync(t=>t.IsActive);
            return result.Select(s => new TaskDto { Description = s.Description, Name = s.Name, Id = s.Id });
        }

        public async Task<IEnumerable<TaskDto>> GetFilteredTask(string searchTerm)
        {
            var result = await _taskRepo.FindAsync(x=>x.IsActive &&( x.Name.Contains(searchTerm) || x.Description.Contains(searchTerm)));
            return result.Select(s => new TaskDto { Description = s.Description, Name = s.Name, Id = s.Id });
        }
    }
}
