using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{
    public Task<IEnumerable<Album>> GetAllAsync();// мне кажется он не нужен
    public Task<Album> GetAlbumByIdAsync(int id);
    public Task<List<Album>> GetAlbumsByArtistAsync(int idArtist, int limit);// возможно по имени артиста нужно искать
    public Task<List<Album>> GetAlbumsByNameAsync(string searchString, int limit);
    public Task<int> AddNewAlbumAsync(Album album);
    public Task<int> UpdateAlbumAsync(Album album);
    public void DeleteAlbumAsync(int id);
}