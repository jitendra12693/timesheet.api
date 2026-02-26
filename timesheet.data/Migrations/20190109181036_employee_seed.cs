using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class employee_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Employees (Code, Name,IsActive,CreatedDate,Password)
                 Employees Values('68319', 'KAYLING',1, GETUTCDATE(),NULL)
                 Employees Values('66928', 'BLAZE',1, GETUTCDATE(),NULL)
                 Employees Values('67832', 'CLARE',1, GETUTCDATE(),NULL)
                 Employees Values('69062', 'JONAS',1, GETUTCDATE(),NULL)
                 Employees Values('63679', 'SCARLET',1, GETUTCDATE(),NULL)
                 Employees Values('64989', 'FRANK',1, GETUTCDATE(),NULL)
                 Employees Values('65271', 'SANDRINE',1, GETUTCDATE(),NULL)
                 Employees Values('66564', 'ADELYN',1, GETUTCDATE(),NULL)
                 Employees Values('68454', 'WADE',1, GETUTCDATE(),NULL)
                 Employees Values('69000', 'MADDEN',1, GETUTCDATE(),NULL)
                  GO  ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
