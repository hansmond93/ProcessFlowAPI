using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class Identity : Migration
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
                name: "ProjectEntities",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    LastModifiedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateTimeCreated = table.Column<DateTime>(nullable: false),
                    DateTimeModified = table.Column<DateTime>(nullable: true),
                    DateTimeDeleted = table.Column<DateTime>(nullable: true),
                    ProjectTitle = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DurationInMonths = table.Column<int>(nullable: false),
                    ProposedAmount = table.Column<decimal>(nullable: false),
                    ApprovedAmount = table.Column<decimal>(nullable: true),
                    Location = table.Column<string>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    ContactAddress = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CompanyEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEntities", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    RoleCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkFLowStatusEntity",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFLowStatusEntity", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ImagesEntities",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublicId = table.Column<string>(nullable: true),
                    DateTimeAdded = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesEntities", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ImagesEntities_ProjectEntities_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectEntities",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLevelEntities",
                columns: table => new
                {
                    ApprovalLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ApprovalLevelEntities_StaffRoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StaffRoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_StaffRoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StaffRoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowTrailEntity",
                columns: table => new
                {
                    WorkFlowTrailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationId = table.Column<int>(nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    FromLevelId = table.Column<int>(nullable: true),
                    ToLevelId = table.Column<int>(nullable: true),
                    ApprovedByStaffId = table.Column<int>(nullable: true),
                    FromStaffId = table.Column<int>(nullable: true),
                    ToStaffId = table.Column<int>(nullable: true),
                    RequestStaffId = table.Column<int>(nullable: false),
                    ApprovalStatusId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DateTimeApproved = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowTrailEntity", x => x.WorkFlowTrailId);
                    table.ForeignKey(
                        name: "FK_WorkFlowTrailEntity_ApprovalStatusEntities_ApprovalStatusId",
                        column: x => x.ApprovalStatusId,
                        principalTable: "ApprovalStatusEntities",
                        principalColumn: "ApprovalStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFlowTrailEntity_ApprovalLevelEntities_FromLevelId",
                        column: x => x.FromLevelId,
                        principalTable: "ApprovalLevelEntities",
                        principalColumn: "ApprovalLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkFlowTrailEntity_OperationEntities_OperationId",
                        column: x => x.OperationId,
                        principalTable: "OperationEntities",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFlowTrailEntity_WorkFLowStatusEntity_StatusId",
                        column: x => x.StatusId,
                        principalTable: "WorkFLowStatusEntity",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFlowTrailEntity_ApprovalLevelEntities_ToLevelId",
                        column: x => x.ToLevelId,
                        principalTable: "ApprovalLevelEntities",
                        principalColumn: "ApprovalLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_StaffRoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StaffRoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "StaffLoginEntities",
                columns: table => new
                {
                    StaffLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffLoginEntities", x => x.StaffLoginId);
                });

            migrationBuilder.CreateTable(
                name: "StaffEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    StaffLoginEntityId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    StaffCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffEntity_StaffRoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StaffRoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffEntity_StaffLoginEntities_StaffLoginEntityId",
                        column: x => x.StaffLoginEntityId,
                        principalTable: "StaffLoginEntities",
                        principalColumn: "StaffLoginId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelEntities_OperationId",
                table: "ApprovalLevelEntities",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelEntities_RoleId",
                table: "ApprovalLevelEntities",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesEntities_ProjectId",
                table: "ImagesEntities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "StaffEntity",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "StaffEntity",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEntity_RoleId",
                table: "StaffEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEntity_StaffLoginEntityId",
                table: "StaffEntity",
                column: "StaffLoginEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffLoginEntities_StaffId",
                table: "StaffLoginEntities",
                column: "StaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "StaffRoleEntity",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_ApprovalStatusId",
                table: "WorkFlowTrailEntity",
                column: "ApprovalStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_ApprovedByStaffId",
                table: "WorkFlowTrailEntity",
                column: "ApprovedByStaffId",
                unique: true,
                filter: "[ApprovedByStaffId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_FromLevelId",
                table: "WorkFlowTrailEntity",
                column: "FromLevelId",
                unique: true,
                filter: "[FromLevelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_FromStaffId",
                table: "WorkFlowTrailEntity",
                column: "FromStaffId",
                unique: true,
                filter: "[FromStaffId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_OperationId",
                table: "WorkFlowTrailEntity",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_RequestStaffId",
                table: "WorkFlowTrailEntity",
                column: "RequestStaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_StatusId",
                table: "WorkFlowTrailEntity",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_ToLevelId",
                table: "WorkFlowTrailEntity",
                column: "ToLevelId",
                unique: true,
                filter: "[ToLevelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowTrailEntity_ToStaffId",
                table: "WorkFlowTrailEntity",
                column: "ToStaffId",
                unique: true,
                filter: "[ToStaffId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowTrailEntity_StaffEntity_ApprovedByStaffId",
                table: "WorkFlowTrailEntity",
                column: "ApprovedByStaffId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowTrailEntity_StaffEntity_FromStaffId",
                table: "WorkFlowTrailEntity",
                column: "FromStaffId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowTrailEntity_StaffEntity_RequestStaffId",
                table: "WorkFlowTrailEntity",
                column: "RequestStaffId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowTrailEntity_StaffEntity_ToStaffId",
                table: "WorkFlowTrailEntity",
                column: "ToStaffId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_StaffEntity_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_StaffEntity_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_StaffEntity_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_StaffEntity_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffLoginEntities_StaffEntity_StaffId",
                table: "StaffLoginEntities",
                column: "StaffId",
                principalTable: "StaffEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffEntity_StaffRoleEntity_RoleId",
                table: "StaffEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffLoginEntities_StaffEntity_StaffId",
                table: "StaffLoginEntities");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ImagesEntities");

            migrationBuilder.DropTable(
                name: "WorkFlowTrailEntity");

            migrationBuilder.DropTable(
                name: "ProjectEntities");

            migrationBuilder.DropTable(
                name: "ApprovalStatusEntities");

            migrationBuilder.DropTable(
                name: "ApprovalLevelEntities");

            migrationBuilder.DropTable(
                name: "WorkFLowStatusEntity");

            migrationBuilder.DropTable(
                name: "OperationEntities");

            migrationBuilder.DropTable(
                name: "StaffRoleEntity");

            migrationBuilder.DropTable(
                name: "StaffEntity");

            migrationBuilder.DropTable(
                name: "StaffLoginEntities");
        }
    }
}
