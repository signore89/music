namespace Music.Models
{
    public class User
    {
        public required int Id {get;set;}
        public required string Name {get;set;}
        public List<Song>? Songs { get; set; } = new List<Song>();
        public List<Artist>? Artists { get; set; } = new List<Artist>();
        public List<Album>? Albums { get; set; } = new List<Album>();
    }
}
