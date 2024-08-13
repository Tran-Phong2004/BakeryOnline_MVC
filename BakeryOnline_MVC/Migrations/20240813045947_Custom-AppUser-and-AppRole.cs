using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryOnline_MVC.Migrations
{
    /// <inheritdoc />
    public partial class CustomAppUserandAppRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DayOfBirth",
                table: "Users",
                type: "datetime",
                nullable: true
            );

            migrationBuilder.AddColumn<DateTime>(
                 name: "CreationTime",
                 table: "Users",
                 type: "datetime",
                 nullable: false,
                 defaultValue: DateTime.UtcNow
            );

            migrationBuilder.AddColumn<bool>(
                name: "IsRoleDefault",
                table: "Roles",
                type: "bit",
                nullable: true,
                defaultValue: "0"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DayOfBirth",
            table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsRoleDefault",
                table: "Roles"
                );
        }
    }
}
