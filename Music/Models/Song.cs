using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Models;

public class Song
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string UrlSong { get; set; }
    public required List<Album> Albums { get; set; }
}