using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class AddedBasicMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffPolicy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ResetDate = table.Column<DateTime>(nullable: false),
                    TimeOffDaysPerYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffRequestApprovalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffRequestApprovalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePositionTimeOffPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    PolicyId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositionTimeOffPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePositionTimeOffPolicies_TimeOffPolicy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "TimeOffPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePositionTimeOffPolicies_TimeOffType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TimeOffType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffPolicyApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    EmployeePositionTimeOffPolicyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffPolicyApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                        column: x => x.EmployeePositionTimeOffPolicyId,
                        principalTable: "EmployeePositionTimeOffPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeOffPolicyApprovals_EmployeePositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    StartsAt = table.Column<DateTime>(nullable: false),
                    EndsOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PolicyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOffRequests_EmployeePositionTimeOffPolicies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "EmployeePositionTimeOffPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeOffRequests_TimeOffType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TimeOffType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffRequestApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    SolvedDate = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    TimeOffRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffRequestApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOffRequestApprovals_TimeOffRequestApprovalStatuses_Statu~",
                        column: x => x.StatusId,
                        principalTable: "TimeOffRequestApprovalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeOffRequestApprovals_TimeOffRequests_TimeOffRequestId",
                        column: x => x.TimeOffRequestId,
                        principalTable: "TimeOffRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TimeOffRequestApprovalStatuses",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Requested" },
                    { 2, "In progres" },
                    { 3, "Denied" },
                    { 4, "Accepted" }
                });

            migrationBuilder.InsertData(
                table: "TimeOffType",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "PayedTimeOff" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTimeOffPolicies_PolicyId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTimeOffPolicies_PositionId",
                table: "EmployeePositionTimeOffPolicies",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTimeOffPolicies_TypeId",
                table: "EmployeePositionTimeOffPolicies",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicyId",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionTimeOffPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicyApprovals_PositionId",
                table: "TimeOffPolicyApprovals",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequestApprovals_StatusId",
                table: "TimeOffRequestApprovals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequestApprovals_TimeOffRequestId",
                table: "TimeOffRequestApprovals",
                column: "TimeOffRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_PolicyId",
                table: "TimeOffRequests",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_TypeId",
                table: "TimeOffRequests",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeOffPolicyApprovals");

            migrationBuilder.DropTable(
                name: "TimeOffRequestApprovals");

            migrationBuilder.DropTable(
                name: "TimeOffRequestApprovalStatuses");

            migrationBuilder.DropTable(
                name: "TimeOffRequests");

            migrationBuilder.DropTable(
                name: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropTable(
                name: "TimeOffPolicy");

            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropTable(
                name: "TimeOffType");
        }
    }
}
