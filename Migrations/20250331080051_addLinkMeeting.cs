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


            migrationBuilder.AddColumn<string>(
                name: "LinkMeeting",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "LinkMeeting",
                table: "Schedules");

            
        }
    }
}
