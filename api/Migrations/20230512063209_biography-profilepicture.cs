using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twitter_clone.Migrations
{
    /// <inheritdoc />
    public partial class biographyprofilepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "UserDTO",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "UserDTO",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "UserDTO");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserDTO");
        }
    }
}
