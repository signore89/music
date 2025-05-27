using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepository(ISongRepository songRepository,IArtistRepository artistRepository) : IAlbumRepository
{

    private List<Album> albums = new()
    {
        new Album()
        {
             Id = 0,
             Name = "Альбом1",
             YearOfIssue = 1999,
             UrlImg = "https://s00.yaplakal.com/pics/pics_preview/9/3/9/6569939.jpg",
             Songs = songRepository.GetSongs()
        }
    };
    public  List<Album> GetAll()
    {
       return  albums;
    }

    public Album GetDetailsById(int id)
    {
        return albums.First(a => a.Id == id);
    }
}