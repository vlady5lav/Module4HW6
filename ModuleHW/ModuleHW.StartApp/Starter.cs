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

                using (var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    db.Database.Migrate();
                    db.SaveChanges();
                }

                using (var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(1);

                    var songsArtistAlive = db.Songs
                        .Where(s => s.GenreId != default)
                        .Join(
                        db.ArtistSongs,
                        s => s.Id,
                        sa => sa.SongId,
                        (s, sa) => new
                        {
                            s.Title,
                            Genre = s.Genre.Title,
                            s.ReleasedDate,
                            s.Duration,
                            sa.Artist,
                        })
                        .ToList()
                        .GroupBy(s => s.Title)
                        .Select(s => new
                        {
                            Title = s.Key,
                            Genre = s.Select(s => s.Genre).FirstOrDefault(),
                            ReleasedDate = s.Select(s => s.ReleasedDate).FirstOrDefault(),
                            Duration = s.Select(s => s.Duration).FirstOrDefault(),
                            Artist = s.Select(s => s.Artist),
                        })
                        .Where(a => !a.Artist.Select(a => a.IsAlived.Value).Contains(false))
                        .ToList();

                    foreach (var t in songsArtistAlive)
                    {
                        Console.WriteLine($"\n--------- Song Title: {t?.Title} ---------\n\n" +
                            $"Song Genre: {t?.Genre}\n" +
                            $"Song Released Date: {t?.ReleasedDate.ToShortDateString()}\n" +
                            $"Song Duration (HH:mm:ss): {TimeSpan.FromSeconds(t?.Duration ?? 0):hh\\:mm\\:ss}");

                        if (t.Artist.Count() == 1)
                        {
                            Console.WriteLine("\nArtist Details:");
                        }
                        else
                        {
                            Console.WriteLine($"\nArtists Details ({t.Artist.Count()}):");
                        }

                        var a = 1;

                        foreach (var s in t.Artist)
                        {
                            Console.WriteLine($"\nArtist {a++}:\n" +
                                $"Artist Name: {s?.Name}\n" +
                                $"Artist Date of Birth: {s?.DateOfBirth.ToShortDateString()}");
                            Print("Artist Date of Death: ", s?.DateOfDeath?.ToShortDateString());
                            Print("Artist Instagram Url: ", s?.InstagramUrl);
                        }

                        Console.WriteLine("\n------- ------- ------- ------- -------");
                    }
                }

                using (var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(2);

                    var genres = db.Songs
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

                    Console.WriteLine($"\nCount of Songs without Genre: {genres.Where(g => g.Title == default).Select(g => g.Count).FirstOrDefault()}");
                }

                using (var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    Header(3);

                    var songsOld = db.Songs
                        .Where(s => s.ReleasedDate < db.Artists.Min(a => a.DateOfBirth))
                        .ToList();

                    foreach (var s in songsOld)
                    {
                        Console.WriteLine($"\n--------- Song Title: {s?.Title} ---------\n\n" +
                            $"Song Genre: {s?.Genre?.Title}\n" +
                            $"Song Released Date: {s?.ReleasedDate.ToShortDateString()}\n" +
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

        public void Print(string s, params string[] i)
        {
            if (s != default && !i.Contains(default))
            {
                Console.Write(s);

                foreach (var str in i)
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
