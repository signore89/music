using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext musicDbContext) : ISongRepository
    {
        public async Task<List<Song>> GetAllAsync()
        {
            var songs = await musicDbContext.Songs.AsNoTracking().ToListAsync();

            return songs;
        }

        public async Task<Song> GetDetailsByIdAsync(int id)
        {
            var song = await musicDbContext.Songs
          .AsNoTracking()
          .FirstAsync(x => x.Id == id);

            return song;
        }
    }
}
