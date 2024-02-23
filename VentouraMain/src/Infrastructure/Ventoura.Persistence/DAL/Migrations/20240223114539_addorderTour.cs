using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventoura.Persistence.DAL.Migrations
{
    public partial class addorderTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourId",
                table: "Orders",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tours_TourId",
                table: "Orders",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tours_TourId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TourId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
