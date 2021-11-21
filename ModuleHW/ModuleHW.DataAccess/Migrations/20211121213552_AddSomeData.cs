using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleHW.DataAccess.Migrations
{
    public partial class AddSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "DateOfBirth", "DateOfDeath", "Email", "InstagramUrl", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "artist1@ma.il", "https://www.instagram.com/artist1", "Artist 1", "380601234567" },
                    { 2, new DateTime(1981, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist2@ma.il", "https://www.instagram.com/artist2", "Artist 2", "380607654321" },
                    { 3, new DateTime(1982, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist3@ma.il", "https://www.instagram.com/artist3", "Artist 3", "380601726345" },
                    { 4, new DateTime(1983, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist4@ma.il", "https://www.instagram.com/artist4", "Artist 4", "380601122334" },
                    { 5, new DateTime(1984, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist5@ma.il", "https://www.instagram.com/artist5", "Artist 5", "380604556677" },
                    { 6, new DateTime(1985, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist6@ma.il", "https://www.instagram.com/artist6", "Artist 6", "380609876543" },
                    { 7, new DateTime(1986, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "artist7@ma.il", "https://www.instagram.com/artist7", "Artist 7", "380603456789" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Title" },
                values: new object[,]
                {
                    { 7, "Genre 7" },
                    { 6, "Genre 6" },
                    { 5, "Genre 5" },
                    { 2, "Genre 2" },
                    { 3, "Genre 3" },
                    { 1, "Genre 1" },
                    { 4, "Genre 4" }
                });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "SongId", "Duration", "GenreId", "ReleasedDate", "Title" },
                values: new object[,]
                {
                    { 7, 700, null, new DateTime(1996, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 7" },
                    { 3, 300, null, new DateTime(1992, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 3" },
                    { 14, 1400, null, new DateTime(1982, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 14" }
                });

            migrationBuilder.InsertData(
                table: "ArtistSong",
                columns: new[] { "ArtistSongId", "ArtistId", "SongId" },
                values: new object[,]
                {
                    { 3, 3, 3 },
                    { 13, 4, 3 },
                    { 7, 7, 7 },
                    { 21, 7, 14 }
                });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "SongId", "Duration", "GenreId", "ReleasedDate", "Title" },
                values: new object[,]
                {
                    { 1, 100, 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 1" },
                    { 13, 1300, 1, new DateTime(1981, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 13" },
                    { 2, 200, 2, new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 2" },
                    { 12, 1200, 2, new DateTime(1980, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 12" },
                    { 11, 1100, 3, new DateTime(1979, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 11" },
                    { 15, 1500, 3, new DateTime(1983, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 15" },
                    { 4, 400, 4, new DateTime(1993, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 4" },
                    { 10, 1000, 4, new DateTime(1978, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 10" },
                    { 5, 500, 5, new DateTime(1994, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 5" },
                    { 9, 900, 5, new DateTime(1977, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 9" },
                    { 6, 600, 6, new DateTime(1995, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 6" },
                    { 8, 800, 6, new DateTime(1976, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Song 8" }
                });

            migrationBuilder.InsertData(
                table: "ArtistSong",
                columns: new[] { "ArtistSongId", "ArtistId", "SongId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 16, 2, 9 },
                    { 14, 4, 5 },
                    { 5, 5, 5 },
                    { 23, 2, 10 },
                    { 17, 3, 10 },
                    { 4, 4, 4 },
                    { 22, 1, 15 },
                    { 24, 3, 11 },
                    { 18, 4, 11 },
                    { 19, 5, 12 },
                    { 12, 4, 2 },
                    { 10, 3, 2 },
                    { 2, 2, 2 },
                    { 20, 6, 13 },
                    { 11, 4, 1 },
                    { 9, 3, 1 },
                    { 8, 2, 1 },
                    { 6, 6, 6 },
                    { 15, 5, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ArtistSong",
                keyColumn: "ArtistSongId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "ArtistId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 6);
        }
    }
}
