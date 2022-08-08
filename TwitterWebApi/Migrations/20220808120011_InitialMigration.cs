using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterWebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tweetMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TweetTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TweetBody = table.Column<string>(type: "nvarchar(144)", maxLength: 144, nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tweetMasters_userRegisters_userId",
                        column: x => x.userId,
                        principalTable: "userRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tweetId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tweetLikes_tweetMasters_tweetId",
                        column: x => x.tweetId,
                        principalTable: "tweetMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tweetLikes_userRegisters_userId",
                        column: x => x.userId,
                        principalTable: "userRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tweetReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    replyTweet = table.Column<string>(type: "nvarchar(144)", maxLength: 144, nullable: false),
                    tweetId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tweetReplies_tweetMasters_tweetId",
                        column: x => x.tweetId,
                        principalTable: "tweetMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_tweetReplies_userRegisters_userId",
                        column: x => x.userId,
                        principalTable: "userRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tweetLikes_tweetId",
                table: "tweetLikes",
                column: "tweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetLikes_userId",
                table: "tweetLikes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetMasters_userId",
                table: "tweetMasters",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetReplies_tweetId",
                table: "tweetReplies",
                column: "tweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetReplies_userId",
                table: "tweetReplies",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tweetLikes");

            migrationBuilder.DropTable(
                name: "tweetReplies");

            migrationBuilder.DropTable(
                name: "tweetMasters");

            migrationBuilder.DropTable(
                name: "userRegisters");
        }
    }
}
