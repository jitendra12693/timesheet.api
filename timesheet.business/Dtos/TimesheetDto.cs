using System;
using System.ComponentModel.DataAnnotations;

namespace timesheet.business.Dtos
{
    public class TimesheetDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please select employee.")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Please select task.")]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
        [Required(ErrorMessage ="Please enter start date and time.")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage ="Please enter end date and time.")]
        public DateTime EndTime { get; set; }
        public string Remarks { get; set; }
    }
}
