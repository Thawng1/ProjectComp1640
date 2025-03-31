using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class addLinkMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "LinkMeeting",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cba8274-4ad0-4954-9e05-5220859db847", null, "Tutor", "TUTOR" },
                    { "121414c1-de28-4dd6-adcd-c1313dd0a5a9", null, "Admin", "ADMIN" },
                    { "867d2be5-cbc6-4d07-bc2b-37097f0580c3", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cba8274-4ad0-4954-9e05-5220859db847");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "121414c1-de28-4dd6-adcd-c1313dd0a5a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "867d2be5-cbc6-4d07-bc2b-37097f0580c3");

            migrationBuilder.DropColumn(
                name: "LinkMeeting",
                table: "Schedules");

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
    }
}
