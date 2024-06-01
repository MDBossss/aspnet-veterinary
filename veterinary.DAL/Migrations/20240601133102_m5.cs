using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 6, 1, 15, 31, 2, 373, DateTimeKind.Local).AddTicks(3650));

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "ID", "BirthDate", "Discriminator", "Name", "OwnerID", "TimesSteppedOn", "Weight" },
                values: new object[] { 6, new DateTime(2024, 6, 1, 15, 31, 2, 374, DateTimeKind.Local).AddTicks(9716), "Hamster", "Donald", 2, 433, 0.40000000000000002 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 6);

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
    }
}
