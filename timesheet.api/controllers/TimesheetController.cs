using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
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
        // GET: api/<TimesheetController>
        [HttpGet("getTimeSheetByEmpId/{id}")]
        public async Task<IActionResult> GetTimeSheetByEmpId(int id)
        {
           var result= await timesheetService.GetTimesheetByEmployeeId(id);
            if(result==null)
                return NotFound();
            return Ok(result);
        }

        // GET api/<TimesheetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TimesheetController>
        [HttpPost("saveTimesheet")]
        public async Task<IActionResult> Post([FromBody]TimesheetDto timesheet)
        {
            var result = await timesheetService.AddTimesheetAsync(timesheet);
            return CreatedAtAction(nameof(GetById),new {id=result.Id},result);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
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
