using System;

using Microsoft.EntityFrameworkCore;

using ModuleHW.DataAccess.Configurations;
using ModuleHW.DataAccess.Models;

namespace ModuleHW.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<ArtistSong> ArtistSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new SongConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistSongConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning);
        }
    }
}
