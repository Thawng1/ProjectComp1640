using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteStudentandBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5b31b9-c5d3-4d21-aaff-993bfbc47a0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5abe1365-216b-494b-8286-6195d809ad33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef4d9ac4-2437-4d5c-a7e3-7b3f436c603b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b48fc89-7c80-4c1a-a521-bda72fca002c", null, "Admin", "ADMIN" },
                    { "8710c3e7-47be-4912-a98d-de6ae8abcdfc", null, "Student", "STUDENT" },
                    { "f430ddbb-4ca5-4ed2-8d0c-0623c1f7ea80", null, "Tutor", "TUTOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b48fc89-7c80-4c1a-a521-bda72fca002c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8710c3e7-47be-4912-a98d-de6ae8abcdfc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f430ddbb-4ca5-4ed2-8d0c-0623c1f7ea80");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e5b31b9-c5d3-4d21-aaff-993bfbc47a0c", null, "Admin", "ADMIN" },
                    { "5abe1365-216b-494b-8286-6195d809ad33", null, "Student", "STUDENT" },
                    { "ef4d9ac4-2437-4d5c-a7e3-7b3f436c603b", null, "Tutor", "TUTOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
