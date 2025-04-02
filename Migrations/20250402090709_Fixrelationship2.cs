using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class Fixrelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropForeignKey(
                 name: "FK_Comments_Blogs_BlogId",
                 table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

        }
 

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b48fc89-7c80-4c1a-a521-bda72fca002c", null, "Admin", "ADMIN" },
                    { "8710c3e7-47be-4912-a98d-de6ae8abcdfc", null, "Student", "STUDENT" },
                    { "f430ddbb-4ca5-4ed2-8d0c-0623c1f7ea80", null, "Tutor", "TUTOR" }
                });
        }
    }
}
