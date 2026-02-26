using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(IBaseService _baseService) : ControllerBase
    {
        [HttpGet("getAllTasks")]
        public async Task<IActionResult> Get()
        {
            var result = await _baseService.GetAllTask();
            if(result == null) 
                return NotFound();
            return Ok(result);
        }

        [HttpGet("searchTask")]
        public async Task<IActionResult> Get(string searchTerm)
        {
            var result = await _baseService.GetFilteredTask(searchTerm);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
