using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.business.Services;

public class BaseServices(IBaseRepository baseRepository) : IBaseService
{
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployee()
    {
        var result = await baseRepository.GetAllEmployee();
        return result.Select(s => new EmployeeDto { Code = s.Code, Name = s.Name, Id = s.Id });
    }

    public async Task<IEnumerable<TaskDto>> GetAllTask()
    {
        var result = await baseRepository.GetAllTask();
        return result.Select(s => new TaskDto { Description = s.Description, Name = s.Name, Id = s.Id });
    }

    public async Task<EmployeeDto> GetEmployeeByCode(string code)
    {
        var result = await baseRepository.GetEmployeeByCode(code);
        return new EmployeeDto { Code = result.Code, Name = result.Name, Id = result.Id };
    }

    public async Task<IEnumerable<TaskDto>> GetFilteredTask(string searchTerm)
    {
        var result = await baseRepository.GetAllTask();
        return result.Select(s => new TaskDto { Description = s.Description, Name = s.Name, Id = s.Id });
    }
}
