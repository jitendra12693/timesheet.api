using System.Collections.Generic;
using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployee();
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);
        Task<string> EmployeeLogin(string code, string password);
    }
}
