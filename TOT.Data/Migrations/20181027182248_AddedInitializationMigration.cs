using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOT.Data.Migrations
{
    public partial class AddedInitializationMigration : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "EmployeePositionTimeOffPolicies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "EmployeePositionTimeOffPolicies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeePositionTimeOffPolicyNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositionTimeOffPolicyNotes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmployeePositionTimeOffPolicyNotes",
                columns: new[] { "Id", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "Оплачиваемый отпуск", "Всего начисляется 20 рабочих дней отпуска в год." },
                    { 2, "Административный (неоплачиваемый) отпуск", "Сотрудникам компании разрешается взять не более 15 рабочих дней в год неоплачиваемого отпуска." },
                    { 3, "Учебный отпуск", "Учебный отпуск можно взять только в период сессии, только тем сотрудникам, кто учится на дневном отделении, и проработал уже 6 месяцев с момента прохождения испытательного срока.<br> Суммарное количество дней учебного отпуска не может превышать 10 рабочих дней в год.<br> На одну сессию нельзя взять больше 5 рабочих дней отпуска." },
                    { 4, "Больничный", "При 1-2 днях отсутствия по болезни можно не брать официальный больничный лист. Таких дней может быть не более 7 в год.<br>Первые 10 суммарных рабочих дней в году, проведенных на больничном(как по больничному листу, так и без него) компания оплачивает в полном 100 % размере.<br>Следующие 10 суммарных рабочих дней в году, проведенных на больничном(как по больничному листу, так и без него) компания оплачивает в 50 % размере.<br>Следующие 10 суммарных рабочих дней в году, проведенных на больничном(как по больничному листу, так и без него) компания оплачивает в 25 % размере.<br>Все последующие дни более 30 рабочих дней компания не оплачивает." }
                });

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "TimeOffPolicy",
                columns: new[] { "Id", "Name", "ResetDate", "TimeOffDaysPerYear" },
                values: new object[,]
                {
                    { 1, "Оплачиваемый отпуск", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 2, "Административный (неоплачиваемый) отпуск", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 3, "Учебный отпуск", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 4, "Больничный", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 }
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
                columns: new[] { "Id", "NoteId", "PolicyId", "PositionId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1, null, 1 },
                    { 2, 2, 2, null, 2 },
                    { 3, 3, 3, null, 3 },
                    { 4, 4, 4, null, 4 }
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
                name: "IX_TimeOffPolicyApprovals_EmployeePositionId",
                table: "TimeOffPolicyApprovals",
                column: "EmployeePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTimeOffPolicies_NoteId",
                table: "EmployeePositionTimeOffPolicies",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositionTimeOffPolic~",
                table: "EmployeePositionTimeOffPolicies",
                column: "NoteId",
                principalTable: "EmployeePositionTimeOffPolicyNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositionTimeOffPolic~",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositionTimeOffPolicies_EmployeePositions_PositionId",
                table: "EmployeePositionTimeOffPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositions_EmployeePositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffPolicyApprovals_EmployeePositionTimeOffPolicies_Emplo~",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropTable(
                name: "EmployeePositionTimeOffPolicyNotes");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffPolicyApprovals_EmployeePositionId",
                table: "TimeOffPolicyApprovals");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePositionTimeOffPolicies_NoteId",
                table: "EmployeePositionTimeOffPolicies");

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
                name: "NoteId",
                table: "EmployeePositionTimeOffPolicies");

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
