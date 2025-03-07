using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e80ebed-5616-4acd-af6b-b01ca9f8147f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ef69a14-bc9a-4765-ac31-22d734e9d832");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "759aaefb-9232-4d0c-9c91-b0b7dfdb3dcc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04b55201-50a4-4ff5-b18c-cc056a7c2914", null, "Student", "STUDENT" },
                    { "082c5df7-fb73-45c8-9933-2c8378f1b34b", null, "Admin", "ADMIN" },
                    { "74251a54-94d4-43ef-a931-a03a4a044ff9", null, "Tutor", "TUTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04b55201-50a4-4ff5-b18c-cc056a7c2914");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "082c5df7-fb73-45c8-9933-2c8378f1b34b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74251a54-94d4-43ef-a931-a03a4a044ff9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e80ebed-5616-4acd-af6b-b01ca9f8147f", null, "Tutor", "TUTOR" },
                    { "3ef69a14-bc9a-4765-ac31-22d734e9d832", null, "Student", "STUDENT" },
                    { "759aaefb-9232-4d0c-9c91-b0b7dfdb3dcc", null, "Admin", "ADMIN" }
                });
        }
    }
}
