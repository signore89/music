using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using static System.Net.WebRequestMethods;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext musicDbContext) : ISongRepository
    {
        public async Task<int> AddNewSongAsync(Song song)
        {
            var myObject = await musicDbContext.Songs.AddAsync(song);
            await musicDbContext.SaveChangesAsync();
            return myObject.Entity.Id;
        }

        public async void DeleteSongAsync(int id)
        {
            var findSong = await musicDbContext.Songs.FindAsync(id);
            if (findSong != null)
            {
                musicDbContext.Songs.Remove(findSong);
                await musicDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            var songs = await musicDbContext.Songs.AsNoTracking().ToListAsync();
            return songs;
        }

        public async Task<Song> GetSongByIdAsync(int id)
        {
            return await musicDbContext.Songs.FirstAsync(a => a.Id == id);
        }

        public async Task<List<Song>> GetSongsByArtistAsync(int idArtist, int limit)
        {
            var songs = await musicDbContext.Songs
                .Include(s => s.Artists.Select(a => a.Id == idArtist))
                .Skip(limit)
                .AsNoTracking()
                .ToListAsync();
            return songs;
        }

        public async Task<List<Song>> GetSongsByNameAsync(string searchString, int limit)
        {
            var songs = await musicDbContext.Songs
           .Where(a => a.Name.Contains(searchString))
           .Skip(limit)
           .AsNoTracking()
           .ToListAsync();
            return songs;
        }

        public async Task<int> UpdateSongAsync(Song song)
        {
            var existingSong = await musicDbContext.Songs
            .Include(s => s.Artists)
            .SingleOrDefaultAsync(s => s.Id == song.Id);
            if (existingSong != null)
            {
                existingSong.Name = song.Name;
                existingSong.UrlSong = song.UrlSong;
                existingSong.AlbumId = song.AlbumId;
                existingSong.Album = song.Album;
                if (song.Artists != null)
                {
                    existingSong.Artists = song.Artists;
                }
                await musicDbContext.SaveChangesAsync();
                return existingSong.Id;
            }
            else
            {
                return 0;//                                                    ДЕЛЬНЫЙ ВОЗВРАТ НУЖЕН
            }
        }
    }
}
