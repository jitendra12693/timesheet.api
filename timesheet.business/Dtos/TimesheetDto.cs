using System;

namespace timesheet.business.Dtos
{
    public class TimesheetDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remarks { get; set; }
    }
}
