namespace ModuleHW.DataAccess.Models
{
    public class ArtistSong
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
    }
}
