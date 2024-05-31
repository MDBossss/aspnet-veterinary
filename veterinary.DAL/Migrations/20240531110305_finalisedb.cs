using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Veterinary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class finalisedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 5, 31, 13, 3, 5, 227, DateTimeKind.Local).AddTicks(4564));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "ID",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2024, 5, 31, 12, 44, 12, 197, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "ID", "Date", "Diagnosis", "Dosage", "PetID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 31, 12, 44, 12, 197, DateTimeKind.Local).AddTicks(8235), "Ear Infection", "Once a day", 1 },
                    { 2, new DateTime(2024, 5, 31, 12, 44, 12, 197, DateTimeKind.Local).AddTicks(8239), "Arthritis", "Every two days", 2 }
                });
        }
    }
}
