using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class BasicMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOffPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeOffTypeId = table.Column<int>(nullable: false),
                    PolicyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOffPolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeOffPolicies_TimeOffTypes_TimeOffTypeId",
                        column: x => x.TimeOffTypeId,
                        principalTable: "TimeOffTypes",
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
                    TimeOffTypeId = table.Column<int>(nullable: false),
                    StartTimeOffDate = table.Column<DateTime>(nullable: false),
                    EndTimeOffDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOffRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOffRequests_TimeOffTypes_TimeOffTypeId",
                        column: x => x.TimeOffTypeId,
                        principalTable: "TimeOffTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccrualSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeAmount = table.Column<double>(nullable: false),
                    TimeMeasureId = table.Column<int>(nullable: false),
                    AmmountAccruedTime = table.Column<double>(nullable: false),
                    AmmountAccruedTimeDates = table.Column<string>(nullable: true),
                    MaxAccrual = table.Column<double>(nullable: false),
                    TimeOffPolicyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccrualSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccrualSchedules_TimeMeasures_TimeMeasureId",
                        column: x => x.TimeMeasureId,
                        principalTable: "TimeMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccrualSchedules_TimeOffPolicies_TimeOffPolicyId",
                        column: x => x.TimeOffPolicyId,
                        principalTable: "TimeOffPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    SolvedDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    TimeOffRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checks_RequestStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checks_TimeOffRequests_TimeOffRequestId",
                        column: x => x.TimeOffRequestId,
                        principalTable: "TimeOffRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RequestStatuses",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Requsted" },
                    { 2, "In progres" },
                    { 3, "Denied" },
                    { 4, "Accepted" }
                });

            migrationBuilder.InsertData(
                table: "TimeMeasures",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Hours" },
                    { 2, "Days" },
                    { 3, "Month" },
                    { 4, "Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccrualSchedules_TimeMeasureId",
                table: "AccrualSchedules",
                column: "TimeMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_AccrualSchedules_TimeOffPolicyId",
                table: "AccrualSchedules",
                column: "TimeOffPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_StatusId",
                table: "Checks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_TimeOffRequestId",
                table: "Checks",
                column: "TimeOffRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicies_PolicyId",
                table: "TimeOffPolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffPolicies_TimeOffTypeId",
                table: "TimeOffPolicies",
                column: "TimeOffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_TimeOffTypeId",
                table: "TimeOffRequests",
                column: "TimeOffTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccrualSchedules");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "TimeMeasures");

            migrationBuilder.DropTable(
                name: "TimeOffPolicies");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "TimeOffRequests");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "TimeOffTypes");
        }
    }
}
