using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllAsync();
        Task<Song> GetDetailsByIdAsync(int id);
    }
}
