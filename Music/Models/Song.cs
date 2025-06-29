namespace Music.Models;

public class Song
{
    public int Id { get; set; }
    public string Name { get; set; } = "unknown song";
    public string UrlSong { get; set; } = "unknown song";
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }
    public List<Artist> Artists { get; set; } = new List<Artist>();
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}