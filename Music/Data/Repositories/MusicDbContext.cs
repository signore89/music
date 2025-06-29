using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories
{
    public class MusicDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
