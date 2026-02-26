using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController(IBaseService _baseService) : ControllerBase
    {
        [HttpGet("getAllEmployee")]
        public async Task<IActionResult> GetAll(string text)
        {
            var result = await _baseService.GetAllEmployee();
            if(result == null) 
                return NotFound();
            return Ok(result);
        }

        [HttpGet("getEmployeeByCode/{code}")]
        public async Task<IActionResult> GetEmployeeByCode(string code)
        {
            var result = await _baseService.GetEmployeeByCode(code);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}