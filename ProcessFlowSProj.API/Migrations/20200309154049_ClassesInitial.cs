using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class ClassesInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalStatusEntities",
                columns: table => new
                {
                    ApprovalStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovalStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatusEntities", x => x.ApprovalStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "OperationEntities",
                columns: table => new
                {
                    OperationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationName = table.Column<string>(nullable: true),
                    OperationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationEntities", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoleEntities",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    RoleCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoleEntities", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowTrailEntities",
                columns: table => new
                {
                    WorkFlowTrailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationId = table.Column<int>(nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    FromLevelId = table.Column<int>(nullable: false),
                    ToLevelId = table.Column<int>(nullable: true),
                    ApprovedByStaffId = table.Column<int>(nullable: false),
                    FromStaffId = table.Column<int>(nullable: false),
                    ToStaffId = table.Column<int>(nullable: true),
                    RequestStaffId = table.Column<int>(nullable: false),
                    ApprovalStatusId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowTrailEntities", x => x.WorkFlowTrailId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLevelEntities",
                columns: table => new
                {
                    ApprovalLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalLevelEntities", x => x.ApprovalLevelId);
                    table.ForeignKey(
                        name: "FK_ApprovalLevelEntities_OperationEntities_OperationId",
                        column: x => x.OperationId,
                        principalTable: "OperationEntities",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffLoginEntity",
                columns: table => new
                {
                    StaffLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffLoginEntity", x => x.StaffLoginId);
                });

            migrationBuilder.CreateTable(
                name: "StaffEntity",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    StaffCode = table.Column<string>(nullable: true),
                    StaffLoginEntityId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffEntity", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_StaffEntity_StaffRoleEntities_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StaffRoleEntities",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffEntity_StaffLoginEntity_StaffLoginEntityId",
                        column: x => x.StaffLoginEntityId,
                        principalTable: "StaffLoginEntity",
                        principalColumn: "StaffLoginId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelEntities_OperationId",
                table: "ApprovalLevelEntities",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEntity_RoleId",
                table: "StaffEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEntity_StaffLoginEntityId",
                table: "StaffEntity",
                column: "StaffLoginEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffLoginEntity_StaffId",
                table: "StaffLoginEntity",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffLoginEntity_StaffEntity_StaffId",
                table: "StaffLoginEntity",
                column: "StaffId",
                principalTable: "StaffEntity",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntity_StaffRoleEntities_RoleId",
                table: "StaffEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntity_StaffLoginEntity_StaffLoginEntityId",
                table: "StaffEntity");

            migrationBuilder.DropTable(
                name: "ApprovalLevelEntities");

            migrationBuilder.DropTable(
                name: "ApprovalStatusEntities");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "WorkFlowTrailEntities");

            migrationBuilder.DropTable(
                name: "OperationEntities");

            migrationBuilder.DropTable(
                name: "StaffRoleEntities");

            migrationBuilder.DropTable(
                name: "StaffLoginEntity");

            migrationBuilder.DropTable(
                name: "StaffEntity");
        }
    }
}
