namespace Music.Models
{
    public class User
    {
        public required int Id {get;set;}
        public required string Name {get;set;}
        public List<Song>? Songs { get; set; }
        public List<Artist>? Artists { get; set; }
        public List<Album>? Albums { get; set; }
    }
}
