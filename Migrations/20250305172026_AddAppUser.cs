using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f2e49a7-109e-49f8-b362-b59fe1c6173e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4740cbb-2a51-44bd-971c-01f66a306747");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebb12007-e49a-4d46-ac0d-995c952f1966");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44c99fa6-b6ee-44fc-abca-bd241c418779", null, "Tutor", "TUTOR" },
                    { "5b251806-d74b-4a08-a0ee-6ad08c09f515", null, "Student", "STUDENT" },
                    { "c4214cdd-b615-4ece-bb54-37b0bdc6f844", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44c99fa6-b6ee-44fc-abca-bd241c418779");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b251806-d74b-4a08-a0ee-6ad08c09f515");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4214cdd-b615-4ece-bb54-37b0bdc6f844");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f2e49a7-109e-49f8-b362-b59fe1c6173e", null, "Student", "STUDENT" },
                    { "c4740cbb-2a51-44bd-971c-01f66a306747", null, "Tutor", "TUTOR" },
                    { "ebb12007-e49a-4d46-ac0d-995c952f1966", null, "Admin", "ADMIN" }
                });
        }
    }
}
