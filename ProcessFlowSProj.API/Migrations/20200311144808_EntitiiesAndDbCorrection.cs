using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class EntitiiesAndDbCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntity_StaffRoleEntities_RoleId",
                table: "StaffEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntity_StaffLoginEntity_StaffLoginEntityId",
                table: "StaffEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffLoginEntity_StaffEntity_StaffId",
                table: "StaffLoginEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffLoginEntity",
                table: "StaffLoginEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffEntity",
                table: "StaffEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entities",
                table: "Entities");

            migrationBuilder.RenameTable(
                name: "StaffLoginEntity",
                newName: "StaffLoginEntities");

            migrationBuilder.RenameTable(
                name: "StaffEntity",
                newName: "StaffEntities");

            migrationBuilder.RenameTable(
                name: "Entities",
                newName: "WorkFLowStatusEntity");

            migrationBuilder.RenameIndex(
                name: "IX_StaffLoginEntity_StaffId",
                table: "StaffLoginEntities",
                newName: "IX_StaffLoginEntities_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffEntity_StaffLoginEntityId",
                table: "StaffEntities",
                newName: "IX_StaffEntities_StaffLoginEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffEntity_RoleId",
                table: "StaffEntities",
                newName: "IX_StaffEntities_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffLoginEntities",
                table: "StaffLoginEntities",
                column: "StaffLoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffEntities",
                table: "StaffEntities",
                column: "StaffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFLowStatusEntity",
                table: "WorkFLowStatusEntity",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffEntities_StaffRoleEntities_RoleId",
                table: "StaffEntities",
                column: "RoleId",
                principalTable: "StaffRoleEntities",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffEntities_StaffLoginEntities_StaffLoginEntityId",
                table: "StaffEntities",
                column: "StaffLoginEntityId",
                principalTable: "StaffLoginEntities",
                principalColumn: "StaffLoginId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffLoginEntities_StaffEntities_StaffId",
                table: "StaffLoginEntities",
                column: "StaffId",
                principalTable: "StaffEntities",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntities_StaffRoleEntities_RoleId",
                table: "StaffEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntities_StaffLoginEntities_StaffLoginEntityId",
                table: "StaffEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffLoginEntities_StaffEntities_StaffId",
                table: "StaffLoginEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFLowStatusEntity",
                table: "WorkFLowStatusEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffLoginEntities",
                table: "StaffLoginEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffEntities",
                table: "StaffEntities");

            migrationBuilder.RenameTable(
                name: "WorkFLowStatusEntity",
                newName: "Entities");

            migrationBuilder.RenameTable(
                name: "StaffLoginEntities",
                newName: "StaffLoginEntity");

            migrationBuilder.RenameTable(
                name: "StaffEntities",
                newName: "StaffEntity");

            migrationBuilder.RenameIndex(
                name: "IX_StaffLoginEntities_StaffId",
                table: "StaffLoginEntity",
                newName: "IX_StaffLoginEntity_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffEntities_StaffLoginEntityId",
                table: "StaffEntity",
                newName: "IX_StaffEntity_StaffLoginEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffEntities_RoleId",
                table: "StaffEntity",
                newName: "IX_StaffEntity_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entities",
                table: "Entities",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffLoginEntity",
                table: "StaffLoginEntity",
                column: "StaffLoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffEntity",
                table: "StaffEntity",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffEntity_StaffRoleEntities_RoleId",
                table: "StaffEntity",
                column: "RoleId",
                principalTable: "StaffRoleEntities",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffEntity_StaffLoginEntity_StaffLoginEntityId",
                table: "StaffEntity",
                column: "StaffLoginEntityId",
                principalTable: "StaffLoginEntity",
                principalColumn: "StaffLoginId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffLoginEntity_StaffEntity_StaffId",
                table: "StaffLoginEntity",
                column: "StaffId",
                principalTable: "StaffEntity",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
