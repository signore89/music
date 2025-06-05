using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Models;

public class Artist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string UrlImg { get; set; }
    //public bool IsFavorite { get; set; }
    public int? UserId { get; set; }
    public required List<Album> Albums { get; set; }
    public required List<Song> Songs { get; set; }

}