using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(int id);
        Task<bool> EditSave(Artist artist);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> CreateAsync(Artist artist);
    }
}
