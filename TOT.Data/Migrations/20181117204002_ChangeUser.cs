using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class ChangeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Employee" });

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
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmployeePositions_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmployeePositions_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 1,
                column: "PositionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 2,
                column: "PositionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 3,
                column: "PositionId",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeePositionTimeOffPolicies",
                keyColumn: "Id",
                keyValue: 4,
                column: "PositionId",
                value: null);
        }
    }
}
