namespace Music.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; } = "unknown artist";
    public  string? UrlImg { get; set; }
    public List<Album>? Albums { get; set; } = new List<Album>();
    public List<Song>? Songs { get; set; } = new List<Song>();
    public List<User>? Users { get; set; } = new List<User>();
}