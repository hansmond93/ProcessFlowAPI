using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class AddedApprovalStatusIColumnToProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatusId",
                table: "ProjectEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEntities_ApprovalStatusId",
                table: "ProjectEntities",
                column: "ApprovalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEntities_ApprovalStatusEntities_ApprovalStatusId",
                table: "ProjectEntities",
                column: "ApprovalStatusId",
                principalTable: "ApprovalStatusEntities",
                principalColumn: "ApprovalStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEntities_ApprovalStatusEntities_ApprovalStatusId",
                table: "ProjectEntities");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEntities_ApprovalStatusId",
                table: "ProjectEntities");

            migrationBuilder.DropColumn(
                name: "ApprovalStatusId",
                table: "ProjectEntities");
        }
    }
}
