using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllAsync();
        Task<Song> GetDetailsByIdAsync(int id);
        Task<List<Song>> SearchByNameAsync(string name);
        Task<bool> EditSave(Song song);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> CreateAsync(Song song);
        Task<List<Song>> GetSongsByIdArtistAsync(int id);
        public Task<List<Song>> GetFavoriteSongs();
    }
}
