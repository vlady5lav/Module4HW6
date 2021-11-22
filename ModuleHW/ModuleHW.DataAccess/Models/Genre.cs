using System.Collections.Generic;

namespace ModuleHW.DataAccess.Models
{
    public class Genre
    {
        public Genre()
        {
            Songs = new List<Song>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
