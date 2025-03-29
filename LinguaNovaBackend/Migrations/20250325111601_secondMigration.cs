using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinguaNovaBackend.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer1",
                table: "VideoTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer2",
                table: "VideoTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer3",
                table: "VideoTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer4",
                table: "VideoTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserVideoProgresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserAudioProgresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserArticleProgresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Answer1",
                table: "AudioTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer2",
                table: "AudioTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer3",
                table: "AudioTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer4",
                table: "AudioTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer1",
                table: "ArticleTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer2",
                table: "ArticleTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer3",
                table: "ArticleTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer4",
                table: "ArticleTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer1",
                table: "VideoTests");

            migrationBuilder.DropColumn(
                name: "Answer2",
                table: "VideoTests");

            migrationBuilder.DropColumn(
                name: "Answer3",
                table: "VideoTests");

            migrationBuilder.DropColumn(
                name: "Answer4",
                table: "VideoTests");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserVideoProgresses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserAudioProgresses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserArticleProgresses");

            migrationBuilder.DropColumn(
                name: "Answer1",
                table: "AudioTests");

            migrationBuilder.DropColumn(
                name: "Answer2",
                table: "AudioTests");

            migrationBuilder.DropColumn(
                name: "Answer3",
                table: "AudioTests");

            migrationBuilder.DropColumn(
                name: "Answer4",
                table: "AudioTests");

            migrationBuilder.DropColumn(
                name: "Answer1",
                table: "ArticleTests");

            migrationBuilder.DropColumn(
                name: "Answer2",
                table: "ArticleTests");

            migrationBuilder.DropColumn(
                name: "Answer3",
                table: "ArticleTests");

            migrationBuilder.DropColumn(
                name: "Answer4",
                table: "ArticleTests");
        }
    }
}
