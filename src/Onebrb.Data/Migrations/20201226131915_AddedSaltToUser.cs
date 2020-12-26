using Microsoft.EntityFrameworkCore.Migrations;

namespace Onebrb.Data.Migrations
{
    public partial class AddedSaltToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecuritySalt",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecuritySalt",
                table: "AspNetUsers");
        }
    }
}
