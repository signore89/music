using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        public Task<IEnumerable<Song>> GetAllAsync();// мне кажется он не нужен
        public Task<Song> GetSongByIdAsync(int id);
        public Task<List<Song>> GetSongsByArtistAsync(int idArtist, int limit);// возможно по имени артиста нужно искать
        public Task<List<Song>> GetSongsByNameAsync(string searchString, int limit);
        public  Task<int> AddNewSongAsync(Song song);
        public Task<int> UpdateSongAsync(Song song);
        public void DeleteSongAsync(int id);
    }
}
