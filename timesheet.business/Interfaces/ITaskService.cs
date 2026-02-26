using System.Collections.Generic;
using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTask();
        Task<IEnumerable<TaskDto>> GetFilteredTask(string searchTerm);
    }
}
