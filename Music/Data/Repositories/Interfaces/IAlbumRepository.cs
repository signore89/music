using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{
    public Task<IEnumerable<Album>> GetAllAsync();// мне кажется он не нужен
    public Task<IEnumerable<Album>> GetPaginationAsync(int quantity, int take);
    public Task<Album> GetAlbumByIdAsync(int? id);
    public Task<List<Album>> GetAlbumsByArtistAsync(int idArtist, int limit);// возможно по имени артиста нужно искать
    public Task<List<Album>> GetAlbumsByNameAsync(string searchString);
    public Task<Album> AddNewAlbumAsync(Album album);
    public Task<Album> UpdateAlbumAsync(Album album);
    public Task<int> DeleteAlbumAsync(int id);
    public Task<List<Album>> GetAlbumsByArtist(int? id,int quantity, int take);
    public Task<int> GetQuantity();
    public Task<int> GetQuantityByArtist(int? artistId);
}