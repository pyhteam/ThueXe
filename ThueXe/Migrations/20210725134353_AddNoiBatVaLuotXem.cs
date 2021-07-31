using Microsoft.EntityFrameworkCore.Migrations;

namespace ThueXe.Migrations
{
    public partial class AddNoiBatVaLuotXem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LuotXem",
                table: "Xe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NoiBat",
                table: "Xe",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LuotXem",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "NoiBat",
                table: "Xe");
        }
    }
}
