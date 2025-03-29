using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinguaNovaBackend.Migrations
{
    /// <inheritdoc />
    public partial class thirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_ArticleTests_ArticleTestId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_Articles_ArticleId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_AudioTests_AudioTestId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_Audios_AudioId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_Users_UserId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_VideoTests_VideoTestId",
                table: "UserTestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTestProgresses_Videos_VideoId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_ArticleId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_ArticleTestId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_AudioId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_AudioTestId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_UserId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_VideoId",
                table: "UserTestProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserTestProgresses_VideoTestId",
                table: "UserTestProgresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_ArticleId",
                table: "UserTestProgresses",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_ArticleTestId",
                table: "UserTestProgresses",
                column: "ArticleTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_AudioId",
                table: "UserTestProgresses",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_AudioTestId",
                table: "UserTestProgresses",
                column: "AudioTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_UserId",
                table: "UserTestProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_VideoId",
                table: "UserTestProgresses",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestProgresses_VideoTestId",
                table: "UserTestProgresses",
                column: "VideoTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_ArticleTests_ArticleTestId",
                table: "UserTestProgresses",
                column: "ArticleTestId",
                principalTable: "ArticleTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_Articles_ArticleId",
                table: "UserTestProgresses",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_AudioTests_AudioTestId",
                table: "UserTestProgresses",
                column: "AudioTestId",
                principalTable: "AudioTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_Audios_AudioId",
                table: "UserTestProgresses",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_Users_UserId",
                table: "UserTestProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_VideoTests_VideoTestId",
                table: "UserTestProgresses",
                column: "VideoTestId",
                principalTable: "VideoTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTestProgresses_Videos_VideoId",
                table: "UserTestProgresses",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
