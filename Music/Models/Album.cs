using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Models;

public class Album
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int YearOfIssue { get; set; }
    public required string UrlImg { get; set; }
    public int ArtistId { get; set; }
    public required List<Song> Songs { get; set; }
}