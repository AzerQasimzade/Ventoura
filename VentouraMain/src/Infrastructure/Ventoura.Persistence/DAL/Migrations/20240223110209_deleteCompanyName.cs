using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventoura.Persistence.DAL.Migrations
{
    public partial class deleteCompanyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
