using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public  List<Album>? Albums { get; set; }
        public  List<Song>? Songs { get; set; }
        public  List<Artist>? Artrists { get; set; }
    }
}
