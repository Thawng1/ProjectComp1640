using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectComp1640.Migrations
{
    /// <inheritdoc />
    public partial class RemoveActionUrlFromNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionUrl",
                table: "Notifications");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "ActionUrl",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
