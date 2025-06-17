using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository(MusicDbContext musicDbContext) : IArtistRepository
    {
        public async Task<int> AddNewArtistAsync(Artist artist)
        {
            var myObject = await musicDbContext.Artists.AddAsync(artist);
            await musicDbContext.SaveChangesAsync();
            return myObject.Entity.Id;
        }

        public async Task<bool> DeleteArtist(int id)
        {
            var findArtist = await musicDbContext.Artists.FindAsync(id);
            if (findArtist != null)
            {
                musicDbContext.Artists.Remove(findArtist);
                await musicDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //public Task<IEnumerable<Artist>> GetAllArtistByAlbumAsync(int idAlbum)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task <IEnumerable<Artist>> GetAllAsync()
        {
            var artists = await musicDbContext.Artists.AsNoTracking().ToListAsync();
            return artists;
        }

        public async Task<Artist> GetArtistByIdAsync(int? id)
        {
            return await musicDbContext.Artists.FirstAsync(a => a.Id == id);
        }

        public async Task<List<Artist>> GetArtistsByNameAsync(string searchString, int limit)
        {
            var artists = await musicDbContext.Artists
           .Where(a => a.Name.Contains(searchString))
           .Skip(limit)
           .AsNoTracking()
           .ToListAsync();
            return artists;
        }

        public async Task<int> UpdateArtistAsync(Artist artist)
        {
            var existingArtist = await musicDbContext.Artists
                .Include(a => a.Songs)
                .Include(a => a.Albums)
                .SingleOrDefaultAsync(a => a.Id == artist.Id);
            if (existingArtist != null)
            {
                existingArtist.Name = artist.Name;
                existingArtist.UrlImg = artist.UrlImg;
                if (artist.Songs != null ||  artist.Albums != null)
                {
                    existingArtist.Albums = artist.Albums;
                    existingArtist.Songs = artist.Songs;
                }
                await musicDbContext.SaveChangesAsync();
                return existingArtist.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
