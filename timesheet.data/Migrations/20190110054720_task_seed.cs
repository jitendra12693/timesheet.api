using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class task_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              INSERT INTO Tasks (Name, Description,IsActive,CreatedDate)
VALUES 
('Sick Leave', 'Apply this task on sick leave.',1, GETUTCDATE()),
('Scrum Ceremonies', 'Scrum meetings, standup, sprint planning, grooming etc.',1, GETUTCDATE()),
('Internal Meeting', 'Meetings Meetings.',1, GETUTCDATE()),
('Development', 'Development tasks, features, change requests.',1, GETUTCDATE()),
('Bug Fixes', 'You know what it means.',1, GETUTCDATE())
                  GO  ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
