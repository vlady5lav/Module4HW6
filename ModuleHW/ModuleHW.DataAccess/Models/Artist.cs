using System;
using System.Collections.Generic;

namespace ModuleHW.DataAccess.Models
{
    public class Artist
    {
        public Artist()
        {
            ArtistSongs = new List<ArtistSong>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public bool? IsAlived { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InstagramUrl { get; set; }
        public virtual List<ArtistSong> ArtistSongs { get; set; }
    }
}
