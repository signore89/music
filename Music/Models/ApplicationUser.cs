using Microsoft.AspNetCore.Identity;

namespace Music.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public List<Song>? Songs { get; set; } = new List<Song>();
        public List<Artist>? Artists { get; set; } = new List<Artist>();
        public List<Album>? Albums { get; set; } = new List<Album>();
    } 
}
