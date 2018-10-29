using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class AddedInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositions_PositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffPolicyApprovals_PositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropColumn(
                name: "ResetDate",
                table: "TimeOffPolicy");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "TimeOffRequests",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffRequestApprovals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffPolicyApprovals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeePositionTimeOffPolicyId",
                table: "TimeOffPolicyApprovals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePositionId",
                table: "TimeOffPolicyApprovals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DelayBeforeAvailable",
                table: "TimeOffPolicy",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "EmployeePositionTimeOffPolicies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "TimeOffPolicy",
                columns: new[] { "Id", "DelayBeforeAvailable", "Name", "TimeOffDaysPerYear" },
                values: new object[,]
                {
                    { 1, 12, "Paid vacation", 20 },
                    { 2, null, "Unpaid vacation", 15 },
                    { 3, 6, "Study vacation", 10 },
                    { 4, null, "Sick vacation", 30 }
                });

            migrationBuilder.UpdateData(
                table: "TimeOffType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Paid Holiday");

            migrationBuilder.InsertData(
                table: "TimeOffType",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 2, "Unpaid leave" },
                    { 3, "Study Holiday" },
                    { 4, "Sick leave" }
                });

            migrationBuilder.InsertData(
                table: "EmployeePositionTimeOffPolicies",
                columns: new[] { "Id", "PolicyId", "PositionId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, null, 1 },
                    { 2, 2, null, 2 },
                    { 3, 3, null, 3 },
                    { 4, 4, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "TimeOffPolicyApprovals",
                columns: new[] { "Id", "Amount", "EmployeePositionId", "EmployeePositionTimeOffPolicyId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, null },
                    { 2, 1, 1, 2, null },
                    { 3, 1, 1, 3, null },
                    { 4, 1, 1, 4, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_UserId",
                table: "TimeOffRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequestApprovals_UserId",
                table: "TimeOffRequestApprovals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_EmployeePositionId",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_UserId",
                table: "TimeOffPolicyApprovals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositions_EmployeePositionId",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionTimeOffPolicyId",
                principalTable: "EmployeePositionTimeOffPolicies",
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
                name: "FK_TimeOffRequestApprovals_AspNetUsers_UserId",
                table: "TimeOffRequestApprovals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_AspNetUsers_UserId",
                table: "TimeOffRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositions_EmployeePositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_AspNetUsers_UserId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequestApprovals_AspNetUsers_UserId",
                table: "TimeOffRequestApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_AspNetUsers_UserId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffRequests_UserId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffRequestApprovals_UserId",
                table: "TimeOffRequestApprovals");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffPolicyApprovals_EmployeePositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffPolicyApprovals_UserId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DeleteData(
                table: "TimeOffPolicyApprovals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicyApprovals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicyApprovals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicyApprovals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicy",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicy",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicy",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TimeOffPolicy",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TimeOffType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TimeOffType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TimeOffType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "EmployeePositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropColumn(
                name: "DelayBeforeAvailable",
                table: "TimeOffPolicy");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TimeOffRequests",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "TimeOffRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffRequestApprovals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffPolicyApprovals",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeePositionTimeOffPolicyId",
                table: "TimeOffPolicyApprovals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "TimeOffPolicyApprovals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetDate",
                table: "TimeOffPolicy",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "EmployeePositionTimeOffPolicies",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TimeOffType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "PayedTimeOff");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_PositionId",
                table: "TimeOffPolicyApprovals",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionTimeOffPolicyId",
                principalTable: "EmployeePositionTimeOffPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositions_PositionId",
                table: "TimeOffPolicyApprovals",
                column: "PositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
