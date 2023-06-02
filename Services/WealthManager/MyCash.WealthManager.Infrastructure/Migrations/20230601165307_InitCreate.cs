using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCash.WealthManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Settings_Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Settings_Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Settings_ExpectedMonthyExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value_Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balances_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SendDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: true),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Families_FamilyId1",
                        column: x => x.FamilyId1,
                        principalTable: "Families",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueNet_Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueNet_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ValueGross_Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueGross_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReceiveDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IncomeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: true),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BalanceEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value_Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BalanceId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BalanceEventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceEvents_Balances_BalanceId",
                        column: x => x.BalanceId,
                        principalTable: "Balances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BalanceEvents_Balances_BalanceId1",
                        column: x => x.BalanceId1,
                        principalTable: "Balances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceEvents_BalanceId",
                table: "BalanceEvents",
                column: "BalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceEvents_BalanceId1",
                table: "BalanceEvents",
                column: "BalanceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Balances_FamilyId",
                table: "Balances",
                column: "FamilyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FamilyId",
                table: "Expenses",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FamilyId1",
                table: "Expenses",
                column: "FamilyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_FamilyId",
                table: "Incomes",
                column: "FamilyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceEvents");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Families");
        }
    }
}
