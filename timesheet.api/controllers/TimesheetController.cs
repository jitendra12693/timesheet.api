using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace timesheet.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController(ITimesheetService timesheetService) : ControllerBase
    {
        [HttpPost("saveTimesheet")]
        public async Task<IActionResult> Post([FromBody]TimesheetDto timesheet)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await timesheetService.AddTimesheetAsync(timesheet);
                return CreatedAtAction(nameof(getTimesheetById), new { id = result.Id }, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {StatusCode=500,Message=ex.Message});
            }
        }

        [HttpGet("getTimesheetById/{id}")]
        public async Task<IActionResult> getTimesheetById(int id)
        {
            var result = await timesheetService.GetTimesheetById(id);
            if (result is null)
                return NotFound();
            return Ok();
        }

        [HttpGet("getAllTimesheet")]
        public async Task<IActionResult> GetAll()
        {
            var result = await timesheetService.GetTimesheets();
            if(result==null)
                return NotFound();
            return Ok(result);
        }
    }
}
