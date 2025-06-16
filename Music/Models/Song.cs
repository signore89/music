namespace Music.Models;

public class Song
{
    public int Id { get; set; }
    public string Name { get; set; } = "unknown song";
    public required string UrlSong { get; set; }
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }
    public List<Artist>? Artists {  get; set; }
    public List<User>? Users { get; set; }
}