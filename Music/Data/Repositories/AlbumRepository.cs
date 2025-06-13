using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepository(MusicDbContext musicDbContext) : IAlbumRepository
{
    public List<Album> GetAll()
    {
        throw new NotImplementedException();
    }

    public Album GetDetailsById(int id)
    {
        throw new NotImplementedException();
    }
}