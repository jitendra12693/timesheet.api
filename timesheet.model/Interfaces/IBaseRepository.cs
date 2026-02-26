using System.Collections.Generic;
using System.Threading.Tasks;

namespace timesheet.model.Interfaces;

public interface IBaseRepository
{
    Task<IEnumerable<Employee>> GetAllEmployee();
    Task<Employee> GetEmployeeByCode(string code);
    Task<IEnumerable<TaskMaster>> GetAllTask();
    Task<IEnumerable<TaskMaster>> GetFilteredTask(string searchTerm);
}
