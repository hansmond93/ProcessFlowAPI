using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class CommentAddedToWorkFlowEntityAndWorkFlowRefactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "StatusId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "ApprovalStatusId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "WorkFlowTrailEntities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "WorkFlowTrailEntities");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "ApprovalStatusId",
                table: "WorkFlowTrailEntities",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
