namespace Music.Models
{
    public class AlbumSong
    {
        public int Id { get; set; }
        public int AlbumsId { get; set; }
        public Album Album { get; set; }

        public int SongsId { get; set; }
        public Song Song { get; set; }
    }
}
