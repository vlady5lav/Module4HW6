using System;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ModuleHW.DataAccess;

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
                        .ToList()
                        .GroupBy(s => s.Title)
                        .Select(s => new
                        {
                            Title = s.Key,
                            Genre = s.Select(s => s.Genre.Title).FirstOrDefault(),
                            ReleasedDate = s.Select(s => s.ReleasedDate).FirstOrDefault(),
                            Duration = s.Select(s => s.Duration).FirstOrDefault(),
                            Artist = s.Select(s => s.ArtistSongs.Select(sa => sa.Artist)).FirstOrDefault(),
                        })
                        .ToList();

                    foreach (var t in songsArtistAlive)
                    {
                        Console.WriteLine($"\n--------- Song Title: {t?.Title ?? "[EMPTY]"} ---------\n\n" +
                            $"Song Genre: {t?.Genre ?? "[EMPTY]"}\n" +
                            $"Song Released Date: {t?.ReleasedDate.ToShortDateString() ?? "EMPTY"}\n" +
                            $"Song Duration (HH:mm:ss): {TimeSpan.FromSeconds(t?.Duration ?? 0):hh\\:mm\\:ss}");

                        if (t?.Artist?.Count() == 1)
                        {
                            Console.WriteLine("\nArtist Details:");
                        }
                        else
                        {
                            Console.WriteLine($"\nArtists Details ({t?.Artist?.Count() ?? 0}):");
                        }

                        var a = 1;

                        foreach (var s in t?.Artist)
                        {
                            Console.WriteLine($"\nArtist {a++}:\n" +
                                $"Artist Name: {s?.Name ?? "[EMPTY]"}\n" +
                                $"Artist Date of Birth: {s?.DateOfBirth.ToShortDateString() ?? "[EMPTY]"}");
                            Print("Artist Date of Death: ", s?.DateOfDeath?.ToShortDateString());
                            Print("Artist Instagram Url: ", s?.InstagramUrl);
                        }

                        Console.WriteLine("\n------- ------- ------- ------- -------");
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

                    Console.WriteLine($"\nCount of Songs without Genre: {genres?.Where(g => g?.Title == default).Select(g => g?.Count)?.FirstOrDefault()}");
                }

                using (var db = serviceProvider?.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(3);

                    var songsOld = db?.Songs
                        .Where(s => s.ReleasedDate < db.Artists.Max(a => a.DateOfBirth))
                        .ToList();

                    foreach (var s in songsOld)
                    {
                        Console.WriteLine($"\n--------- Song Title: {s?.Title ?? "[EMPTY]"} ---------\n\n" +
                            $"Song Genre: {s?.Genre?.Title ?? "[EMPTY]"}\n" +
                            $"Song Released Date: {s?.ReleasedDate.ToShortDateString() ?? "[EMPTY]"}\n" +
                            $"Song Duration (HH:mm:ss): {TimeSpan.FromSeconds(s?.Duration ?? 0):hh\\:mm\\:ss}");

                        Console.WriteLine("\n------- ------- ------- ------- -------");
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
                foreach (var str in s)
                {
                    Console.Write(str);
                }

                Console.WriteLine(string.Empty);
            }
        }

        public void Header(int s)
        {
            Console.WriteLine("\n\n-----------------------------------------------------");
            Console.WriteLine($"---------------------- Query {s} ----------------------");
            Console.WriteLine("-----------------------------------------------------\n");
        }
    }
}
