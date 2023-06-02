using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCash.WealthManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Families_FamilyId",
                table: "Incomes");

            migrationBuilder.RenameColumn(
                name: "ValueNet_Currency",
                table: "Incomes",
                newName: "Value_Currency");

            migrationBuilder.RenameColumn(
                name: "ValueNet_Count",
                table: "Incomes",
                newName: "Value_Count");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                table: "Incomes",
                newName: "OperationDate");

            migrationBuilder.RenameColumn(
                name: "IncomeType",
                table: "Incomes",
                newName: "TransferType");

            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Expenses",
                newName: "OperationDate");

            migrationBuilder.RenameColumn(
                name: "ExpenseType",
                table: "Expenses",
                newName: "TransferType");

            migrationBuilder.AlterColumn<Guid>(
                name: "FamilyId",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId1",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_FamilyId1",
                table: "Incomes",
                column: "FamilyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Families_FamilyId",
                table: "Incomes",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Families_FamilyId1",
                table: "Incomes",
                column: "FamilyId1",
                principalTable: "Families",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Families_FamilyId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Families_FamilyId1",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_FamilyId1",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "FamilyId1",
                table: "Incomes");

            migrationBuilder.RenameColumn(
                name: "Value_Currency",
                table: "Incomes",
                newName: "ValueNet_Currency");

            migrationBuilder.RenameColumn(
                name: "Value_Count",
                table: "Incomes",
                newName: "ValueNet_Count");

            migrationBuilder.RenameColumn(
                name: "TransferType",
                table: "Incomes",
                newName: "IncomeType");

            migrationBuilder.RenameColumn(
                name: "OperationDate",
                table: "Incomes",
                newName: "ReceiveDate");

            migrationBuilder.RenameColumn(
                name: "TransferType",
                table: "Expenses",
                newName: "ExpenseType");

            migrationBuilder.RenameColumn(
                name: "OperationDate",
                table: "Expenses",
                newName: "SendDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "FamilyId",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Families_FamilyId",
                table: "Incomes",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
