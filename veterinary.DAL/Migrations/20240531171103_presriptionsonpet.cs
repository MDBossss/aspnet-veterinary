using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class presriptionsonpet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 5, 31, 19, 11, 3, 429, DateTimeKind.Local).AddTicks(4096));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 5, 31, 13, 3, 5, 227, DateTimeKind.Local).AddTicks(4564));
        }
    }
}
