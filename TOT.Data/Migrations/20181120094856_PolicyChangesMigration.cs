using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class PolicyChangesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffPolicy_PolicyId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffType_TypeId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_AspNetUsers_UserId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_TimeOffType_TypeId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffPolicyApprovals_UserId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffType",
                table: "TimeOffType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffPolicy",
                table: "TimeOffPolicy");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.RenameTable(
                name: "TimeOffType",
                newName: "TimeOffTypes");

            migrationBuilder.RenameTable(
                name: "TimeOffPolicy",
                newName: "TimeOffPolicies");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeePositionTimeOffPolicies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NextPolicyId",
                table: "EmployeePositionTimeOffPolicies",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffTypes",
                table: "TimeOffTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffPolicies",
                table: "TimeOffPolicies",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "PositionId" },
                values: new object[] { true, null });

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsActive", "PositionId" },
                values: new object[] { true, null });

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsActive", "PositionId" },
                values: new object[] { true, null });

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsActive", "PositionId" },
                values: new object[] { true, null });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTimeOffPolicies_NextPolicyId",
                table: "EmployeePositionTimeOffPolicies",
                column: "NextPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositionTimeOffPolic~",
                table: "EmployeePositionTimeOffPolicies",
                column: "NextPolicyId",
                principalTable: "EmployeePositionTimeOffPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffPolicies_PolicyId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PolicyId",
                principalTable: "TimeOffPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffTypes_TypeId",
                table: "EmployeePositionTimeOffPolicies",
                column: "TypeId",
                principalTable: "TimeOffTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_TimeOffTypes_TypeId",
                table: "TimeOffRequests",
                column: "TypeId",
                principalTable: "TimeOffTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositionTimeOffPolic~",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffPolicies_PolicyId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffTypes_TypeId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_TimeOffTypes_TypeId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePositionTimeOffPolicies_NextPolicyId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffTypes",
                table: "TimeOffTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffPolicies",
                table: "TimeOffPolicies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropColumn(
                name: "NextPolicyId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.RenameTable(
                name: "TimeOffTypes",
                newName: "TimeOffType");

            migrationBuilder.RenameTable(
                name: "TimeOffPolicies",
                newName: "TimeOffPolicy");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TimeOffPolicyApprovals",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffType",
                table: "TimeOffType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffPolicy",
                table: "TimeOffPolicy",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 1,
                column: "PositionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 2,
                column: "PositionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 3,
                column: "PositionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 4,
                column: "PositionId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_UserId",
                table: "TimeOffPolicyApprovals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffPolicy_PolicyId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PolicyId",
                principalTable: "TimeOffPolicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_TimeOffType_TypeId",
                table: "EmployeePositionTimeOffPolicies",
                column: "TypeId",
                principalTable: "TimeOffType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffPolicyApprovals_AspNetUsers_UserId",
                table: "TimeOffPolicyApprovals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_TimeOffType_TypeId",
                table: "TimeOffRequests",
                column: "TypeId",
                principalTable: "TimeOffType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
