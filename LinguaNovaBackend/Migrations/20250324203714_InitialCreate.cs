using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinguaNovaBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleTests_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioTests_Audios_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArticleProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticleProgresses_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticleProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAudioProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudioProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAudioProgresses_Audios_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAudioProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVideoProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideoProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVideoProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideoProgresses_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoTests_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: true),
                    VideoId = table.Column<int>(type: "INTEGER", nullable: true),
                    AudioId = table.Column<int>(type: "INTEGER", nullable: true),
                    ArticleTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    VideoTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    AudioTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_ArticleTests_ArticleTestId",
                        column: x => x.ArticleTestId,
                        principalTable: "ArticleTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_AudioTests_AudioTestId",
                        column: x => x.AudioTestId,
                        principalTable: "AudioTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_Audios_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_VideoTests_VideoTestId",
                        column: x => x.VideoTestId,
                        principalTable: "VideoTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTestProgresses_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTests_ArticleId",
                table: "ArticleTests",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioTests_AudioId",
                table: "AudioTests",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleProgresses_ArticleId",
                table: "UserArticleProgresses",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleProgresses_UserId",
                table: "UserArticleProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAudioProgresses_AudioId",
                table: "UserAudioProgresses",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAudioProgresses_UserId",
                table: "UserAudioProgresses",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoProgresses_UserId",
                table: "UserVideoProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoProgresses_VideoId",
                table: "UserVideoProgresses",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTests_VideoId",
                table: "VideoTests",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArticleProgresses");

            migrationBuilder.DropTable(
                name: "UserAudioProgresses");

            migrationBuilder.DropTable(
                name: "UserTestProgresses");

            migrationBuilder.DropTable(
                name: "UserVideoProgresses");

            migrationBuilder.DropTable(
                name: "ArticleTests");

            migrationBuilder.DropTable(
                name: "AudioTests");

            migrationBuilder.DropTable(
                name: "VideoTests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Audios");

            migrationBuilder.DropTable(
                name: "Videos");
        }
    }
}
