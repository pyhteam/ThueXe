using Microsoft.EntityFrameworkCore.Migrations;

namespace ThueXe.Migrations
{
    public partial class InitialEditImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Xe");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Xe",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Xe");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Xe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
