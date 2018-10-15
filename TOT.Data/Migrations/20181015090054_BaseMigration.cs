using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class BaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TimeOffRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(nullable: true),
                    TimeOffTypeId = table.Column<int>(nullable: false),
                    StartTimeOffDate = table.Column<DateTimeOffset>(nullable: false),
                    EndTimeOffDate = table.Column<DateTimeOffset>(nullable: false),
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
                name: "Checks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Manager = table.Column<string>(nullable: true),
                    SolvedDate = table.Column<DateTimeOffset>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Checks_StatusId",
                table: "Checks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_TimeOffRequestId",
                table: "Checks",
                column: "TimeOffRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_TimeOffTypeId",
                table: "TimeOffRequests",
                column: "TimeOffTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "TimeOffRequests");

            migrationBuilder.DropTable(
                name: "TimeOffTypes");
        }
    }
}
