using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories
{
    public class MusicDbContext(DbContextOptions<MusicDbContext> options) : DbContext(options)
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<AlbumSong> AlbumSongs { get; set; }
        public DbSet<ArtistSongs> ArtistSongs { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Album>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Song>().Property(s => s.Id).ValueGeneratedOnAdd();
        }
    }
}
