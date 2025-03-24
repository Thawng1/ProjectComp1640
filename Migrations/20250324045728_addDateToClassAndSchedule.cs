using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class addDateToClassAndSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "242c39e6-621e-4373-a18f-2be49090d5ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "599c6165-e94a-4cfa-a65b-de5477a3abba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab912df8-9587-47eb-9bb5-26aaccc1ab80");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "941e2050-9a7d-45f5-b857-3b564ed89ca4", null, "Student", "STUDENT" },
                    { "eb282888-05cb-42d4-a962-91a43aeb3556", null, "Admin", "ADMIN" },
                    { "fcf07427-8db7-4ae2-9f1a-a776fb1931d7", null, "Tutor", "TUTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "941e2050-9a7d-45f5-b857-3b564ed89ca4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb282888-05cb-42d4-a962-91a43aeb3556");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcf07427-8db7-4ae2-9f1a-a776fb1931d7");

            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Classes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "242c39e6-621e-4373-a18f-2be49090d5ab", null, "Student", "STUDENT" },
                    { "599c6165-e94a-4cfa-a65b-de5477a3abba", null, "Admin", "ADMIN" },
                    { "ab912df8-9587-47eb-9bb5-26aaccc1ab80", null, "Tutor", "TUTOR" }
                });
        }
    }
}
