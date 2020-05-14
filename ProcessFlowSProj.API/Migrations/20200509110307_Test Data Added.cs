using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessFlowSProj.API.Migrations
{
    public partial class TestDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT[dbo].[OperationEntities] ON");
            migrationBuilder.Sql("INSERT[dbo].[OperationEntities] ([OperationId], [OperationName], [OperationCode]) VALUES(1, N'TestOperation', N'TO')");
            migrationBuilder.Sql("INSERT[dbo].[OperationEntities] ([OperationId], [OperationName], [OperationCode]) VALUES(2, N'TestOperationTwo', N'TOTw')");
            migrationBuilder.Sql("INSERT[dbo].[OperationEntities] ([OperationId], [OperationName], [OperationCode]) VALUES(3, N'TestOperationThree', N'TOTh')");
            migrationBuilder.Sql("SET IDENTITY_INSERT[dbo].[OperationEntities] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[RoleEntity] ON");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (-1, N'administrator', N'A')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (1, N'account Officer', N'AO')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (2, N'Branch Manager', N'BM')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (3, N'Risk Officer', N'RO')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (4, N'Vault Officer', N'VO')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (5, N'Relationship Mnanager', N'RM')");
            migrationBuilder.Sql("INSERT [dbo].[RoleEntity] ([Id], [RoleName], [RoleCode]) VALUES (6, N'Money Manager', N'MM')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[RoleEntity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[ApprovalLevelEntities] ON");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (2, 1, 1, 1, 2)");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (3, 2, 1, 1, 3)");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (4, 1, 2, 1, 4)");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (5, 1, 3, 1, 3)");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (6, 1, 4, 0, 5)");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalLevelEntities] ([ApprovalLevelId], [OperationId], [Position], [Active], [RoleEntityId]) VALUES (7, 1, 5, 1, 6)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[ApprovalLevelEntities] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[ApprovalStatusEntities] ON");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalStatusEntities] ([ApprovalStatusId], [ApprovalStatusName]) VALUES (0, N'Pending')");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalStatusEntities] ([ApprovalStatusId], [ApprovalStatusName]) VALUES (1, N'Processing')");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalStatusEntities] ([ApprovalStatusId], [ApprovalStatusName]) VALUES (2, N'Approved')");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalStatusEntities] ([ApprovalStatusId], [ApprovalStatusName]) VALUES (3, N'Rejected')");
            migrationBuilder.Sql("INSERT [dbo].[ApprovalStatusEntities] ([ApprovalStatusId], [ApprovalStatusName]) VALUES (4, N'Referred')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[ApprovalStatusEntities] OFF");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
