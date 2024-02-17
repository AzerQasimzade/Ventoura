using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventoura.Persistence.DAL.Migrations
{
    public partial class StatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tours");
        }
    }
}
