using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        public Task<IEnumerable<Artist>> GetAllAsync();// мне кажется он не нужен
        public Task<Artist> GetArtistByIdAsync(int id);
        public Task<List<Artist>> GetArtistsByNameAsync(string searchString, int limit);
        public Task<int> AddNewArtistAsync(Artist artist);
        public Task<int> UpdateArtistAsync(Artist artist);
        public void DeleteArtist(int id);
    }
}
