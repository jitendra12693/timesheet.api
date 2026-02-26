using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.business.Services;

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

        [HttpPost("registerEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _employeeService.AddEmployeeAsync(employee);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { StatusCode = 500, Message = ex.Message });
            }
        }

        [HttpPost("employeeLogin")]
        public async Task<IActionResult> EmployeeLogin([FromBody] EmployeeLoginDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _employeeService.EmployeeLogin(employee.Code,employee.Password);
                return Ok(new { StatusCode = 200, Token = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { StatusCode = 500, Message = ex.Message });
            }
        }

    }
}