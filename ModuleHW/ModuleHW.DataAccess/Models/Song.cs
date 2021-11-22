using System;
using System.Collections.Generic;

namespace ModuleHW.DataAccess.Models
{
    public class Song
    {
        public Song()
        {
            ArtistSongs = new List<ArtistSong>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<ArtistSong> ArtistSongs { get; set; }
    }
}
