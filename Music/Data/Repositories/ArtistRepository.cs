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
            return await musicDbContext.Artists
                .Include(a => a.Albums)
                .Include(a => a.Songs)
                .FirstAsync(a => a.Id == id);
        }

        public async Task<List<Artist>> GetArtistsByNameAsync(string searchString)
        {
            var artists = await musicDbContext.Artists
           .Where(a => a.Name.Contains(searchString))
           .AsNoTracking()
           .ToListAsync();
            return artists;
        }

        public async Task<IEnumerable<Artist>> GetPaginationAsync(int quantity, int take)
        {
            var artists = await musicDbContext.Artists.Skip(quantity).Take(take).AsNoTracking().ToListAsync();
            return artists;
        }

        public async Task<int> GetQuantity()
        {
            var temp = await musicDbContext.Artists.CountAsync();
            return temp;
        }

        public async Task<Artist> UpdateArtistAsync(Artist artist, int[] selectedSongs, int[] selectedAlbums)
        {
            var existingArtist = await musicDbContext.Artists
                .Include(a => a.Songs)
                .Include(a => a.Albums)
                .SingleOrDefaultAsync(a => a.Id == artist.Id);
            if (existingArtist != null)
            {
                existingArtist.Name = artist.Name;
                existingArtist.UrlImg = artist.UrlImg;
                
                if (selectedAlbums.Any())
                {
                    existingArtist.Albums.Clear();
                    foreach (var album in musicDbContext.Albums.Where(a => selectedAlbums.Contains(a.Id)))
                    {
                        existingArtist.Albums.Add(album);
                    }
                }
                if (selectedSongs.Any())
                {
                    existingArtist.Songs.Clear();
                    foreach (var song in musicDbContext.Songs.Where(s => selectedSongs.Contains(s.Id)))
                    {
                        existingArtist.Songs.Add(song);
                    }
                }
                await musicDbContext.SaveChangesAsync();
                return existingArtist;
            }
            else
            {
                return null;
            }
        }
    }
}
