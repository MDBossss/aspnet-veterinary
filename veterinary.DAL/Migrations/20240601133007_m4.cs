using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Pets",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<int>(
                name: "TimesSteppedOn",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 6, 1, 15, 30, 7, 47, DateTimeKind.Local).AddTicks(8950));

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "ID", "BirthDate", "Discriminator", "Name", "OwnerID", "TimesSteppedOn", "Weight" },
                values: new object[] { 3, new DateTime(2024, 6, 1, 15, 30, 7, 49, DateTimeKind.Local).AddTicks(1977), "Hamster", "Donald", 2, 433, 0.40000000000000002 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "TimesSteppedOn",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Pets",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 6, 1, 15, 21, 3, 794, DateTimeKind.Local).AddTicks(4554));
        }
    }
}
