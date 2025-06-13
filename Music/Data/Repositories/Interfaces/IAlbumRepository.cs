using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{
    public List<Album> GetAll();// мне кажется он не нужен
    public Album GetAlbumById(int id);
    public List<Album> GetAlbumsByArtist(int idArtist, int limit);// возможно по имени артиста нужно искать
    public List<Album> GetAlbumsByName(string searchString, int limit);
    public int AddNewAlbum(Album album);
    public int UpdateAlbum(Album album);
    public int DeleteAlbum(int id);
}