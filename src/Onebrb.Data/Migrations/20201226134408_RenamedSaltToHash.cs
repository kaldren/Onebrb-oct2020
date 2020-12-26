using Microsoft.EntityFrameworkCore.Migrations;

namespace Onebrb.Data.Migrations
{
    public partial class RenamedSaltToHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecuritySalt",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SecurityHash",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityHash",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SecuritySalt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
