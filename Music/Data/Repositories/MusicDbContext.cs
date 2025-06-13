using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories
{
    public class MusicDbContext(DbContextOptions<MusicDbContext> options) : DbContext(options)
    {
        public DbSet<Artist> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
