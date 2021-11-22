using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ModuleHW.DataAccess.Models;

namespace ModuleHW.DataAccess.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist").HasKey(a => a.Id);

            builder.Property(a => a.Id).HasColumnName("ArtistId")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name).HasColumnName("Name")
                .IsRequired();

            builder.Property(a => a.DateOfBirth).HasColumnName("DateOfBirth")
                .HasColumnType("date").IsRequired();

            builder.Property(a => a.DateOfDeath).HasColumnName("DateOfDeath")
                .HasColumnType("date").IsRequired(false);

            builder.Property(a => a.IsAlived).HasColumnName("IsAlived")
                .HasComputedColumnSql(
                "cast (case when DateOfDeath is null then (1) else (0) end as bit)")
                .IsRequired();

            builder.Property(a => a.Phone).HasColumnName("Phone")
                .HasMaxLength(15).IsRequired(false);

            builder.Property(a => a.Email).HasColumnName("Email")
                .HasMaxLength(320).IsRequired(false);

            builder.Property(a => a.InstagramUrl).HasColumnName("InstagramUrl")
                .IsRequired(false);

            builder.HasData(new List<Artist>()
            {
                new Artist()
                {
                    Id = 1,
                    Name = "Artist 1",
                    DateOfBirth = new DateTime(1980, 01, 01),
                    DateOfDeath = new DateTime(1983, 01, 01),
                    Phone = "380601234567",
                    Email = "artist1@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist1",
                },

                new Artist()
                {
                    Id = 2,
                    Name = "Artist 2",
                    DateOfBirth = new DateTime(1981, 02, 02),
                    Phone = "380607654321",
                    Email = "artist2@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist2",
                },

                new Artist()
                {
                    Id = 3,
                    Name = "Artist 3",
                    DateOfBirth = new DateTime(1982, 03, 03),
                    Phone = "380601726345",
                    Email = "artist3@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist3",
                },

                new Artist()
                {
                    Id = 4,
                    Name = "Artist 4",
                    DateOfBirth = new DateTime(1983, 04, 04),
                    Phone = "380601122334",
                    Email = "artist4@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist4",
                },

                new Artist()
                {
                    Id = 5,
                    Name = "Artist 5",
                    DateOfBirth = new DateTime(1984, 05, 05),
                    Phone = "380604556677",
                    Email = "artist5@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist5",
                },

                new Artist()
                {
                    Id = 6,
                    Name = "Artist 6",
                    DateOfBirth = new DateTime(1985, 06, 06),
                    Phone = "380609876543",
                    Email = "artist6@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist6",
                },

                new Artist()
                {
                    Id = 7,
                    Name = "Artist 7",
                    DateOfBirth = new DateTime(1986, 07, 07),
                    Phone = "380603456789",
                    Email = "artist7@ma.il",
                    InstagramUrl = "https://www.instagram.com/artist7",
                },
            });
        }
    }
}
