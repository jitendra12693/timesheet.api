using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController(IEmployeeService _employeeService) : ControllerBase
    {
        [HttpGet("getAllEmployee")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetAllEmployee();
            if(result == null) 
                return NotFound();
            return Ok(result);
        }
    }
}