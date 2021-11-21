using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ModuleHW.DataAccess.Models;

namespace ModuleHW.DataAccess.Configurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Song").HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName("SongId").ValueGeneratedOnAdd();

            builder.Property(s => s.Title).HasColumnName("Title").IsRequired();
            builder.Property(s => s.Duration).HasColumnName("Duration").IsRequired();
            builder.Property(s => s.ReleasedDate).HasColumnName("ReleasedDate").HasColumnType("date").IsRequired();

            builder.HasOne(s => s.Genre).WithMany(g => g.Songs)
                .HasForeignKey(s => s.GenreId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasData(new List<Song>()
            {
                new Song() { Id = 1, Title = "Song 1", Duration = 100, GenreId = 1, ReleasedDate = new DateTime(1990, 01, 01), },
                new Song() { Id = 2, Title = "Song 2", Duration = 200, GenreId = 2, ReleasedDate = new DateTime(1991, 02, 02), },
                new Song() { Id = 3, Title = "Song 3", Duration = 300, ReleasedDate = new DateTime(1992, 03, 03), },
                new Song() { Id = 4, Title = "Song 4", Duration = 400, GenreId = 4, ReleasedDate = new DateTime(1993, 04, 04), },
                new Song() { Id = 5, Title = "Song 5", Duration = 500, GenreId = 5, ReleasedDate = new DateTime(1994, 05, 05), },
                new Song() { Id = 6, Title = "Song 6", Duration = 600, GenreId = 6, ReleasedDate = new DateTime(1995, 06, 06), },
                new Song() { Id = 7, Title = "Song 7", Duration = 700, ReleasedDate = new DateTime(1996, 07, 07), },
                new Song() { Id = 8, Title = "Song 8", Duration = 800, GenreId = 6, ReleasedDate = new DateTime(1976, 08, 08), },
                new Song() { Id = 9, Title = "Song 9", Duration = 900, GenreId = 5, ReleasedDate = new DateTime(1977, 09, 09), },
                new Song() { Id = 10, Title = "Song 10", Duration = 1000, GenreId = 4, ReleasedDate = new DateTime(1978, 10, 10), },
                new Song() { Id = 11, Title = "Song 11", Duration = 1100, GenreId = 3, ReleasedDate = new DateTime(1979, 11, 11), },
                new Song() { Id = 12, Title = "Song 12", Duration = 1200, GenreId = 2, ReleasedDate = new DateTime(1980, 12, 12), },
                new Song() { Id = 13, Title = "Song 13", Duration = 1300, GenreId = 1, ReleasedDate = new DateTime(1981, 11, 13), },
                new Song() { Id = 14, Title = "Song 14", Duration = 1400, ReleasedDate = new DateTime(1982, 10, 14), },
                new Song() { Id = 15, Title = "Song 15", Duration = 1500, GenreId = 3, ReleasedDate = new DateTime(1983, 09, 15), },
            });
        }
    }
}
