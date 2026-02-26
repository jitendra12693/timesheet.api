using System.Collections.Generic;
using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface IBaseService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployee();
        Task<EmployeeDto> GetEmployeeByCode(string code);
        Task<IEnumerable<TaskDto>> GetAllTask();
        Task<IEnumerable<TaskDto>> GetFilteredTask(string searchTerm);
    }
}
