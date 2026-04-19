using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DvdLibrary.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandSeedDataAndUseLocalSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DvdMovies",
                columns: new[] { "Id", "Director", "DurationMinutes", "GenreId", "IsAvailable", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 4, "Ridley Scott", 117, 1, true, 1982, "Blade Runner" },
                    { 5, "Christopher Nolan", 148, 1, true, 2010, "Inception" },
                    { 6, "George Miller", 120, 2, true, 2015, "Mad Max: Fury Road" },
                    { 7, "John McTiernan", 132, 2, true, 1988, "Die Hard" },
                    { 8, "Frank Darabont", 142, 3, true, 1994, "The Shawshank Redemption" },
                    { 9, "Ron Howard", 135, 3, false, 2001, "A Beautiful Mind" },
                    { 10, "Frank Darabont", 189, 3, true, 1999, "The Green Mile" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DvdMovies",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
