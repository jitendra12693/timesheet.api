using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.data.Repository;
using timesheet.model;
using timesheet.model.Interfaces;

namespace timesheet.business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _empRepo;
        public EmployeeService(IBaseRepository<Employee> empRepo)
        {
            _empRepo = empRepo;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployee()
        {
            var result = await _empRepo.GetAllAsync();
            return result.Select(s => new EmployeeDto { Code = s.Code, Name = s.Name, Id = s.Id });
        }
    }
}
