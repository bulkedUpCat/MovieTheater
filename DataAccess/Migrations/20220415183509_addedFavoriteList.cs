using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addedFavoriteList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteList",
                columns: table => new
                {
                    FavoriteListMoviesId = table.Column<int>(type: "int", nullable: false),
                    FavoriteListUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteList", x => new { x.FavoriteListMoviesId, x.FavoriteListUsersId });
                    table.ForeignKey(
                        name: "FK_FavoriteList_Movies_FavoriteListMoviesId",
                        column: x => x.FavoriteListMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteList_Users_FavoriteListUsersId",
                        column: x => x.FavoriteListUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteList_FavoriteListUsersId",
                table: "FavoriteList",
                column: "FavoriteListUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteList");
        }
    }
}
