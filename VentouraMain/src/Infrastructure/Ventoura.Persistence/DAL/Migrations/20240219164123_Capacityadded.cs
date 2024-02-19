using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventoura.Persistence.DAL.Migrations
{
    public partial class Capacityadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Tours");
        }
    }
}
