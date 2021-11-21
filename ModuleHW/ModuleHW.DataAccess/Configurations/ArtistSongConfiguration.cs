using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ModuleHW.DataAccess.Models;

namespace ModuleHW.DataAccess.Configurations
{
    public class ArtistSongConfiguration : IEntityTypeConfiguration<ArtistSong>
    {
        public void Configure(EntityTypeBuilder<ArtistSong> builder)
        {
            builder.ToTable("ArtistSong").HasKey(a => a.Id);

            builder.Property(a => a.Id).HasColumnName("ArtistSongId").ValueGeneratedOnAdd();

            builder.HasOne(a => a.Artist).WithMany(a => a.ArtistSongs)
                .HasForeignKey(a => a.ArtistId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Song).WithMany(s => s.ArtistSongs)
                .HasForeignKey(a => a.SongId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<ArtistSong>()
            {
                new ArtistSong() { Id = 1, ArtistId = 1, SongId = 1, },
                new ArtistSong() { Id = 2, ArtistId = 2, SongId = 2, },
                new ArtistSong() { Id = 3, ArtistId = 3, SongId = 3, },
                new ArtistSong() { Id = 4, ArtistId = 4, SongId = 4, },
                new ArtistSong() { Id = 5, ArtistId = 5, SongId = 5, },
                new ArtistSong() { Id = 6, ArtistId = 6, SongId = 6, },
                new ArtistSong() { Id = 7, ArtistId = 7, SongId = 7, },
                new ArtistSong() { Id = 8, ArtistId = 2, SongId = 1, },
                new ArtistSong() { Id = 9, ArtistId = 3, SongId = 1, },
                new ArtistSong() { Id = 10, ArtistId = 3, SongId = 2, },
                new ArtistSong() { Id = 11, ArtistId = 4, SongId = 1, },
                new ArtistSong() { Id = 12, ArtistId = 4, SongId = 2, },
                new ArtistSong() { Id = 13, ArtistId = 4, SongId = 3, },
                new ArtistSong() { Id = 14, ArtistId = 4, SongId = 5, },
                new ArtistSong() { Id = 15, ArtistId = 5, SongId = 8, },
                new ArtistSong() { Id = 16, ArtistId = 2, SongId = 9, },
                new ArtistSong() { Id = 17, ArtistId = 3, SongId = 10, },
                new ArtistSong() { Id = 18, ArtistId = 4, SongId = 11, },
                new ArtistSong() { Id = 19, ArtistId = 5, SongId = 12, },
                new ArtistSong() { Id = 20, ArtistId = 6, SongId = 13, },
                new ArtistSong() { Id = 21, ArtistId = 7, SongId = 14, },
                new ArtistSong() { Id = 22, ArtistId = 1, SongId = 15, },
                new ArtistSong() { Id = 23, ArtistId = 2, SongId = 10, },
                new ArtistSong() { Id = 24, ArtistId = 3, SongId = 11, },
            });
        }
    }
}
