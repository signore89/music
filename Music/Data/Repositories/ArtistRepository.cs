using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository(MusicDbContext musicDbContext) : IArtistRepository
    {
        public async Task<List<Artist>> GetAllAsync()
        {
            var artists = await musicDbContext.Artists.AsNoTracking().ToListAsync();

            return artists;
        }

        public async Task<Artist> GetDetailsByIdAsync(int id)
        {
            var artist = await musicDbContext.Artists
                    .AsNoTracking()
                    .Include(artist => artist.Songs)
                    .FirstAsync(x => x.Id == id);

            return artist;
        }
    }
}
