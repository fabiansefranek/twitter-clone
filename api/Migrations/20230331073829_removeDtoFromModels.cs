using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twitter_clone.Migrations
{
    /// <inheritdoc />
    public partial class removeDtoFromModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_UserDTO_FollowedId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_UserDTO_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_PostDTO_PostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_UserDTO_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDTO_UserDTO_UserId",
                table: "PostDTO");

            migrationBuilder.DropIndex(
                name: "IX_PostDTO_UserId",
                table: "PostDTO");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowedId",
                table: "Follows",
                column: "FollowedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowedId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_PostDTO_UserId",
                table: "PostDTO",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_UserDTO_FollowedId",
                table: "Follows",
                column: "FollowedId",
                principalTable: "UserDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_UserDTO_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "UserDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_PostDTO_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "PostDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_UserDTO_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "UserDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostDTO_UserDTO_UserId",
                table: "PostDTO",
                column: "UserId",
                principalTable: "UserDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
