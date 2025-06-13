using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepository(MusicDbContext musicDbContext) : IAlbumRepository
{
    public List<Artist> GetAll()
    {
        throw new NotImplementedException();
    }

    public Artist GetAlbumById(int id)
    {
        throw new NotImplementedException();
    }

    List<Album> IAlbumRepository.GetAll()
    {
        throw new NotImplementedException();
    }

    Album IAlbumRepository.GetAlbumById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Album> GetAlbumsByArtist(int idArtist, int limit)
    {
        throw new NotImplementedException();
    }

    public List<Album> GetAlbumsByName(string searchString, int limit)
    {
        throw new NotImplementedException();
    }

    public int AddNewAlbum(Album album)
    {
        throw new NotImplementedException();
    }

    public int UpdateAlbum(Album album)
    {
        throw new NotImplementedException();
    }

    public int DeleteAlbum(int id)
    {
        throw new NotImplementedException();
    }
}