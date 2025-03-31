using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class FixEmailandnullUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c542ffd-2f67-42f1-8ba0-40b30270da4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "750eb95f-fee8-4475-bb72-641ad785d4f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f1bda12-ec7f-443b-b483-83fee7ddcfe5");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e5b31b9-c5d3-4d21-aaff-993bfbc47a0c", null, "Admin", "ADMIN" },
                    { "5abe1365-216b-494b-8286-6195d809ad33", null, "Student", "STUDENT" },
                    { "ef4d9ac4-2437-4d5c-a7e3-7b3f436c603b", null, "Tutor", "TUTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c542ffd-2f67-42f1-8ba0-40b30270da4e", null, "Tutor", "TUTOR" },
                    { "750eb95f-fee8-4475-bb72-641ad785d4f9", null, "Admin", "ADMIN" },
                    { "9f1bda12-ec7f-443b-b483-83fee7ddcfe5", null, "Student", "STUDENT" }
                });
        }
    }
}
