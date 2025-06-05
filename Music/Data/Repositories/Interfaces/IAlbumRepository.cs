using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAsync();
    Task<Album> GetDetailsByIdAsync(int id);
    Task<List<Album>> SearchByNameAsync(string name);
    Task<List<Album>> SearchArtistAlbums(int id);   
}