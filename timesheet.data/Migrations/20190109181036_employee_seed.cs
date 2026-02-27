using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class employee_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Employees (Code, Name, IsActive, CreatedDate, Password) VALUES" +
                 "('68319', 'KAYLING',1, GETUTCDATE(),'"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('66928', 'BLAZE',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('67832', 'CLARE',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('69062', 'JONAS',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('63679', 'SCARLET',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('64989', 'FRANK',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('65271', 'SANDRINE',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('66564', 'ADELYN',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('68454', 'WADE',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')"+
                 ",('69000', 'MADDEN',1, GETUTCDATE(), '"+BCrypt.Net.BCrypt.HashPassword("Welcome@123")+"')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
