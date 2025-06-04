using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext musicDbContext) : ISongRepository
    {
        public async Task<bool> CreateAsync(Song song)
        {
            musicDbContext.Songs.Add(song);
            await musicDbContext.SaveChangesAsync();
            return true;// добавить подтверждения
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var findSong = await musicDbContext.Songs.FindAsync(id);
            if (findSong != null)
            {
                musicDbContext.Songs.Remove(findSong);
                await musicDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditSave(Song song)
        {
            var existingSong = await musicDbContext.Songs
                     .FirstAsync(x => x.Id == song.Id);
            if (existingSong != null)
            {
                existingSong.Name = song.Name;
                existingSong.UrlSong = song.UrlSong;
                existingSong.AlbumId = song.AlbumId;
                existingSong.Artists = song.Artists;
                musicDbContext.Update(existingSong);
                await musicDbContext.SaveChangesAsync();
                return true;
            }
            return false;

            // добавить проверку на успех, а также перенести бизнес логику в контроллер
        }

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

        public async Task<List<Song>> GetSongsByIdArtistAsync(int id)
        {
            return await musicDbContext.ArtistSongs
                .Where(a => a.ArtistId == id)
                .Include(s => s.Song)
                .Select(s => s.Song)
                .ToListAsync();
        }

        public async Task<List<Song>> SearchByNameAsync(string name)
        {
            return await musicDbContext.Songs
            .Where(a => a.Name.Contains(name))
            .ToListAsync();
        }
    }
}
