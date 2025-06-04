using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Music.Data.Repositories
{
    public class AlbumRepository(MusicDbContext musicDbContext) : IAlbumRepository
    {
        public async Task<List<Album>> GetAllAsync()
        {
            var albums = await musicDbContext.Albums.AsNoTracking().ToListAsync();

            return albums;
        }

        public async Task<Album> GetDetailsByIdAsync(int id)
        {
            var album = await musicDbContext.Albums
                    .AsNoTracking()
                    .Include(album => album.Songs)
                    .FirstAsync(x => x.Id == id);

            return album;
        }

        public async Task<List<Album>> SearchByNameAsync(string name)
        {
           return await musicDbContext.Albums
                    .Where(a => a.Name.Contains(name))
                    .ToListAsync();
        }
    }
}
