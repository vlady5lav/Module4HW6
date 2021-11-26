using System;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ModuleHW.DataAccess;
using ModuleHW.DataAccess.Models;

namespace ModuleHW.StartApplication
{
    public class Starter
    {
        public void Run()
        {
            var configFile = "appsettings.json";
            var configFileInfo = new FileInfo(configFile);

            if (configFileInfo.Exists)
            {
                IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(configFile).Build();
                var connectionString = configuration.GetConnectionString("VSMusicDB");

                var serviceProvider = new ServiceCollection()
                    .AddDbContext<ApplicationContext>(optionsDb => optionsDb
                    .UseSqlServer(connectionString, optionsSql => optionsSql
                    .CommandTimeout(30))
                    .UseLazyLoadingProxies())
                    .AddOptions()
                    .BuildServiceProvider();

                using (var db = serviceProvider?.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    db?.Database?.Migrate();
                    db?.SaveChanges();
                }

                using (var db = serviceProvider?.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(1);

                    var songsArtistAlive = db?.Songs
                        .Where(s => s.GenreId != default)
                        .Where(s => s.ArtistSongs.All(a => a.Artist.IsAlived == true))
                        .ToList();

                    foreach (var s in songsArtistAlive)
                    {
                        SongInfo(s);
                    }
                }

                using (var db = serviceProvider?.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(2);

                    var genres = db?.Songs
                        .GroupBy(s => s.Genre.Title)
                        .Select(g => new
                        {
                            Title = g.Key,
                            Count = g.Count(),
                        })
                        .ToList();

                    Console.WriteLine($"\nGenres:\n");

                    foreach (var t in genres)
                    {
                        Print($"Count of Songs with Genre Title \"", t?.Title?.ToString(), "\": ", t?.Count.ToString());
                    }

                    var songsCountNoGenre = genres?
                        .Where(g => g.Title == default)
                        .Select(g => g.Count)
                        .SingleOrDefault();

                    Console.WriteLine($"\nCount of Songs without Genre: {songsCountNoGenre}");
                }

                using (var db = serviceProvider?.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(3);

                    var songsOld = db?.Songs
                        .Where(s => s.ReleasedDate < db.Artists.Max(a => a.DateOfBirth))
                        .ToList();

                    foreach (var s in songsOld)
                    {
                        SongInfo(s);
                    }
                }
            }
            else
            {
                Console.WriteLine($"ERROR! There is no config file \"{configFile}\" provided!");
                Environment.Exit(0);
            }
        }

        public void Print(params string[] s)
        {
            if (!s.Contains(default))
            {
                Console.WriteLine(string.Join(string.Empty, s));
            }
        }

        public void Header(int s)
        {
            Console.WriteLine("\n-----------------------------------------------------");
            Console.WriteLine($"---------------------- Query {s} ----------------------");
            Console.WriteLine("-----------------------------------------------------\n");
        }

        public void SongInfo(Song s)
        {
            var artists = s?.ArtistSongs?.Where(sa => sa?.SongId == s?.Id).Select(a => a?.Artist).ToList();

            var artistsName = string.Join(", ", artists?.Select(a => a.Name).OrderBy(a => a));

            var artistsCount = artists.Count;

            Console.WriteLine(
                "\n" +
                "-------- -------- ------- -------- --------");

            Console.WriteLine(
                $"\n" +
                $"  Song : {artistsName ?? "[Unknown]"} - {s?.Title ?? "[Unknown]"}" +
                $"\n");

            Console.WriteLine(
                $"  -----------------------------------------" +
                $"\n" +
                $"  | Title: {s?.Title ?? "[Unknown]"}" +
                $"\n" +
                $"  |    Genre: {s?.Genre?.Title ?? "[EMPTY]"}" +
                $"\n" +
                $"  |    Released Date: {s?.ReleasedDate.ToShortDateString() ?? "EMPTY"}" +
                $"\n" +
                $"  |    Duration (hh:mm:ss): {TimeSpan.FromSeconds(s?.Duration ?? 0):hh\\:mm\\:ss}" +
                $"\n" +
                $"  |    Artists Count: {artistsCount}" +
                $"\n" +
                $"  -----------------------------------------");

            var i = 1;

            foreach (var a in artists)
            {
                Console.WriteLine(
                    $"  -----------------------------------------" +
                    $"\n" +
                    $"  | Artist {i++}" +
                    $"\n" +
                    $"  |    Name: {a?.Name ?? "[EMPTY]"}" +
                    $"\n" +
                    $"  |    Date of Birth: {a?.DateOfBirth.ToShortDateString() ?? "[EMPTY]"}" +
                    $"\n" +
                    $"  |    IsAlive: {a?.IsAlived.ToString() ?? "[EMPTY]"}");

                Print("  |    Date of Death: ", a?.DateOfDeath?.ToShortDateString());
                Print("  |    Phone Number: +", a?.Phone);
                Print("  |    E-Mail Adress: ", a?.Email);
                Print("  |    Instagram Url: ", a?.InstagramUrl);
                Console.WriteLine($"  -----------------------------------------");
            }
        }
    }
}
