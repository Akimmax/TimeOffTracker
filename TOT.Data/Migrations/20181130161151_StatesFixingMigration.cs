using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class StatesFixingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TimeOffRequestApprovalStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Queued");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TimeOffRequestApprovalStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "In progres");
        }
    }
}
