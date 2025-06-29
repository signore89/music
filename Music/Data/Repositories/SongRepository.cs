using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext musicDbContext) : ISongRepository
    {
        public async Task<Song> AddNewSongAsync(Song song)
        {
            var myObject = musicDbContext.Songs.Add(song).Entity;
            await musicDbContext.SaveChangesAsync();
            return myObject;
        }

        public async Task<List<Artist>> AddSongsByArtists(int idSong,Artist artist)
        {
            var song = await GetSongByIdAsync(idSong);
            song.Artists.Add(artist);
            await musicDbContext.SaveChangesAsync();
            return song.Artists;
        }

        public async Task<int?> DeleteSongAsync(int id)
        {
            var findSong = await musicDbContext.Songs.FindAsync(id);
            if (findSong != null)
            {
                musicDbContext.Songs.Remove(findSong);
                await musicDbContext.SaveChangesAsync();
                return findSong.AlbumId;
            }
            return 0;
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            var songs = await musicDbContext.Songs.AsNoTracking().ToListAsync();
            
            return songs;
        }

        public async Task<IEnumerable<Song>> GetPaginationAsync(int quantity, int take)
        {
            var songs = await musicDbContext.Songs.Skip(quantity).Take(take).AsNoTracking().ToListAsync();
            return songs;
        }

        public async Task<int> GetQuantity()
        {
            var temp = await musicDbContext.Songs.CountAsync();
            return temp;
        }

        public async Task<List<Song>> GetSongByAlbum(int albumId)
        {
            var songs = await musicDbContext.Songs
                .Where(s => s.AlbumId == albumId)
                .AsNoTracking()
                .ToListAsync();
            return songs;
        }

        public async Task<Song> GetSongByIdAsync(int? id)
        {
            return await musicDbContext.Songs
                .Include(s => s.Artists)
                .Include(s => s.Album)
                .FirstAsync(a => a.Id == id);
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

        public async Task<List<Song>> GetSongsByNameAsync(string searchString)
        {
            var songs = await musicDbContext.Songs
           .Where(a => a.Name.Contains(searchString))
           .AsNoTracking()
           .ToListAsync();
            return songs;
        }

        public async Task<int> UpdateSongAsync(Song song, int[] selectedArtists)
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
               
                if (selectedArtists.Any())
                {
                    existingSong.Artists.Clear();
                    foreach (var artist in musicDbContext.Artists.Where(a => selectedArtists.Contains(a.Id)))
                    {
                        existingSong.Artists.Add(artist);
                    }
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
