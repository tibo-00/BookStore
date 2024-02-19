using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    public partial class BookUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenreId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "GenreId1",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId1",
                table: "Books",
                column: "GenreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId1",
                table: "Books",
                column: "GenreId1",
                principalTable: "Genres",
                principalColumn: "Id");
        }
    }
}
