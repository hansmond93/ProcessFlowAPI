using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class InitialClassesAfterApprovalCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalLevelEntities_StaffEntity_StaffId",
                table: "ApprovalLevelEntities");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "ApprovalLevelEntities",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalLevelEntities_StaffId",
                table: "ApprovalLevelEntities",
                newName: "IX_ApprovalLevelEntities_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalLevelEntities_StaffRoleEntities_RoleId",
                table: "ApprovalLevelEntities",
                column: "RoleId",
                principalTable: "StaffRoleEntities",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalLevelEntities_StaffRoleEntities_RoleId",
                table: "ApprovalLevelEntities");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ApprovalLevelEntities",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalLevelEntities_RoleId",
                table: "ApprovalLevelEntities",
                newName: "IX_ApprovalLevelEntities_StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalLevelEntities_StaffEntity_StaffId",
                table: "ApprovalLevelEntities",
                column: "StaffId",
                principalTable: "StaffEntity",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
