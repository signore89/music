using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository(MusicDbContext musicDbContext) : IArtistRepository
    {
        public async Task<bool> CreateAsync(Artist artist)
        {
            musicDbContext.Artists.Add(artist);
            await musicDbContext.SaveChangesAsync();
            return true;// добавить подтверждения
        }

        public async Task<bool> DeleteByIdAsync(int id)
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

        public async Task<bool> EditSave(Artist artist)
        {
            var existingArtist = await musicDbContext.Artists
                    .FirstAsync(x => x.Id == artist.Id);
            if (existingArtist != null)
            {
                existingArtist.Name = artist.Name;
                existingArtist.UrlImg = artist.UrlImg;
                existingArtist.UserId = artist.UserId;
                musicDbContext.Update(existingArtist);
                await musicDbContext.SaveChangesAsync();
                return true;
            }
            return false;

             // добавить проверку на успех, а также перенести бизнес логику в контроллер
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            var artists = await musicDbContext.Artists.AsNoTracking().ToListAsync();

            return artists;
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await musicDbContext.Artists
                    .FirstAsync(x => x.Id == id);

            return artist;
        }

        public async Task<List<Artist>> SearchByNameAsync(string name)
        {
            return await musicDbContext.Artists 
            .Where(a => a.Name.Contains(name))
            .ToListAsync();
        }
    }
}
