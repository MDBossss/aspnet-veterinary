using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Medication",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ActiveIngredient",
                table: "Medication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DosageUnit",
                table: "Medication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideEffect",
                table: "Medication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medication",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ActiveIngredient", "DosageUnit", "SideEffect" },
                values: new object[] { "Amoxicillin Trihydrate", "mg", "Nausea, diarrhea, vomiting (less common)" });

            migrationBuilder.UpdateData(
                table: "Medication",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ActiveIngredient", "DosageUnit", "SideEffect" },
                values: new object[] { "Prednisolone", "mg", "Increased appetite, weight gain, mood swings (less common)" });

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 6, 2, 12, 59, 2, 329, DateTimeKind.Local).AddTicks(6909));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 6,
                column: "BirthDate",
                value: new DateTime(2024, 6, 2, 12, 59, 2, 331, DateTimeKind.Local).AddTicks(9023));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveIngredient",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "DosageUnit",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "SideEffect",
                table: "Medication");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Medication",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 6, 1, 15, 31, 2, 373, DateTimeKind.Local).AddTicks(3650));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 6,
                column: "BirthDate",
                value: new DateTime(2024, 6, 1, 15, 31, 2, 374, DateTimeKind.Local).AddTicks(9716));
        }
    }
}
