namespace Music.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<Album> Albums { get; set; }
        public required List<Song> Songs { get; set; }
        public required List<Artist> Artrists { get; set; }
    }
}
