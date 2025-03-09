using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36169913-fe41-400e-8c61-3053ad1f84a8", null, "Tutor", "TUTOR" },
                    { "58fa9cbb-f985-467d-a6a1-d8592a7d8cc6", null, "Admin", "ADMIN" },
                    { "c9422078-701b-4a84-b7d8-44f2dd348e81", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36169913-fe41-400e-8c61-3053ad1f84a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58fa9cbb-f985-467d-a6a1-d8592a7d8cc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9422078-701b-4a84-b7d8-44f2dd348e81");
        }
    }
}
