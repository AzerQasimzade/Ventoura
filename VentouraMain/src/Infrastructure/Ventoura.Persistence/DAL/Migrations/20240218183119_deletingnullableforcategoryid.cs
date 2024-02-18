using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventoura.Persistence.DAL.Migrations
{
    public partial class deletingnullableforcategoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Category_CategoryId",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Category_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Category_CategoryId",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tours",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Category_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
