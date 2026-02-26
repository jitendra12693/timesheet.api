using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Interfaces;
using timesheet.model.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IBaseService _baseService;
        public EmployeeController(IBaseService baseService)
        {
            _baseService = baseService;
        }
        private readonly EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

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