using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updatedWatchLater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchLaterMovies");

            migrationBuilder.CreateTable(
                name: "WatchLater",
                columns: table => new
                {
                    WatchLaterMoviesId = table.Column<int>(type: "int", nullable: false),
                    WatchLaterUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLater", x => new { x.WatchLaterMoviesId, x.WatchLaterUsersId });
                    table.ForeignKey(
                        name: "FK_WatchLater_Movies_WatchLaterMoviesId",
                        column: x => x.WatchLaterMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchLater_Users_WatchLaterUsersId",
                        column: x => x.WatchLaterUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchLater_WatchLaterUsersId",
                table: "WatchLater",
                column: "WatchLaterUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchLater");

            migrationBuilder.CreateTable(
                name: "WatchLaterMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLaterMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchLaterMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchLaterMovies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaterMovies_MovieId",
                table: "WatchLaterMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaterMovies_UserId",
                table: "WatchLaterMovies",
                column: "UserId");
        }
    }
}
