namespace Music.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; } = "unknown artist";
    public  string? UrlImg { get; set; }
    public List<Album>? Albums { get; set; }
    public List<Song>? Songs { get; set; }
    public List<User>? Users { get; set; }
}