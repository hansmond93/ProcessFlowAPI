using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class updatedEntitiesWithImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FromStaffId",
                table: "WorkFlowTrailEntities",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.CreateIndex(
                name: "IX_ImagesEntities_ProjectId",
                table: "ImagesEntities",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesEntities");

            migrationBuilder.DropTable(
                name: "ProjectEntities");

            migrationBuilder.AlterColumn<int>(
                name: "FromStaffId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
