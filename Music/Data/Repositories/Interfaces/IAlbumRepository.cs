using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{
    public List<Album> GetAll();
    public Album GetDetailsById(int id);
}