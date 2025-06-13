namespace Music.Models;

public class Album
{
    public int Id { get; set; }
    public required string Name { get; set; } = "unknown album";
    public  int? YearOfIssue { get; set; }
    public  string? UrlImg { get; set; }
    public int ArtistId { get; set; }
    public required Artist Artist { get; set; }
    public  List<Song>? Songs { get; set; }
    public List<User>? Users { get; set; }

}