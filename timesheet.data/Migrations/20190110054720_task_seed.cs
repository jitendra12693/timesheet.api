using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class task_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              INSERT INTO Tasks (Name, Description)
VALUES 
('Sick Leave', 'Apply this task on sick leave.'),
('Scrum Ceremonies', 'Scrum meetings, standup, sprint planning, grooming etc.'),
('Internal Meeting', 'Meetings Meetings.'),
('Development', 'Development tasks, features, change requests.'),
('Bug Fixes', 'You know what it means.')
                  GO  ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
