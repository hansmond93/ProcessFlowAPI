using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class InitialClasseswithFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FromLevelId",
                table: "WorkFlowTrailEntities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedByStaffId",
                table: "WorkFlowTrailEntities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "ApprovalLevelEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntities_OperationId",
                table: "WorkFlowTrailEntities",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelEntities_StaffId",
                table: "ApprovalLevelEntities",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalLevelEntities_StaffEntity_StaffId",
                table: "ApprovalLevelEntities",
                column: "StaffId",
                principalTable: "StaffEntity",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowTrailEntities_OperationEntities_OperationId",
                table: "WorkFlowTrailEntities",
                column: "OperationId",
                principalTable: "OperationEntities",
                principalColumn: "OperationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalLevelEntities_StaffEntity_StaffId",
                table: "ApprovalLevelEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowTrailEntities_OperationEntities_OperationId",
                table: "WorkFlowTrailEntities");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowTrailEntities_OperationId",
                table: "WorkFlowTrailEntities");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalLevelEntities_StaffId",
                table: "ApprovalLevelEntities");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "ApprovalLevelEntities");

            migrationBuilder.AlterColumn<int>(
                name: "FromLevelId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedByStaffId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
