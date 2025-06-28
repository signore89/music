using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        public Task<IEnumerable<Artist>> GetAllAsync();// мне кажется он не нужен
        public Task<Artist> GetArtistByIdAsync(int? id);
        public Task<List<Artist>> GetArtistsByNameAsync(string searchString, int limit);
        public Task<int> AddNewArtistAsync(Artist artist);
        public Task<Artist> UpdateArtistAsync(Artist artist, int[] selectedSongs, int[] selectedAlbums);
        public Task<bool> DeleteArtist(int id);
        //public Task<IEnumerable<Artist>> GetAllArtistByAlbumAsync(int idAlbum);
    }
}
