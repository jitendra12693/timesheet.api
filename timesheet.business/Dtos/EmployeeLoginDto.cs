using System.ComponentModel.DataAnnotations;

namespace timesheet.business.Dtos
{
    public class EmployeeLoginDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
