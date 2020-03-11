using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class FixedUsernameInOneEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "StaffLoginEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "StaffLoginEntity",
                nullable: false,
                defaultValue: "");
        }
    }
}
