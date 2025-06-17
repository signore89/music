using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepository(MusicDbContext musicDbContext) : IAlbumRepository
{
    public async Task<IEnumerable<Album>> GetAllAsync()
    {
        var albums = await musicDbContext.Albums.AsNoTracking().ToListAsync();
        return albums;
    }

    public async Task<Album> GetAlbumByIdAsync(int? id)
    {
        return await musicDbContext.Albums.FirstAsync(a => a.Id == id);
    }

    public async Task<List<Album>> GetAlbumsByArtistAsync(int idArtist, int limit)
    {
        var albums = await musicDbContext.Albums
            .Where(a => a.ArtistId == idArtist)
            .Skip(limit)
            .AsNoTracking()
            .ToListAsync();
        return albums;
    }

    public async Task<List<Album>> GetAlbumsByNameAsync(string searchString, int limit)
    {
        var albums = await musicDbContext.Albums
            .Where(a => a.Name.Contains(searchString))
            .Skip(limit)
            .AsNoTracking()
            .ToListAsync();
        return albums;
    }

    public async Task<int> AddNewAlbumAsync(Album album)
    {
        var myObject = await musicDbContext.Albums.AddAsync(album);
        await musicDbContext.SaveChangesAsync();
        return myObject.Entity.Id;
    }

    public async Task<int> UpdateAlbumAsync(Album album)
    {
        var existingAlbum = await musicDbContext.Albums
            .Include(a => a.Songs)
            .SingleOrDefaultAsync(a => a.Id == album.Id);
        if (existingAlbum != null)
        {
            existingAlbum.Name = album.Name;
            existingAlbum.YearOfIssue = album.YearOfIssue;
            existingAlbum.UrlImg = album.UrlImg;
            existingAlbum.ArtistId = album.ArtistId;
            existingAlbum.Artist = album.Artist;
            if (album.Songs != null)
            {
                existingAlbum.Songs = album.Songs;
            }
            await musicDbContext.SaveChangesAsync();
            return existingAlbum.Id;
        } 
        else
        {
            return 0;//                                                    ДЕЛЬНЫЙ ВОЗВРАТ НУЖЕН
        }
    }

    public async Task DeleteAlbumAsync(int id)
    {
        var findAlbum = await musicDbContext.Albums.FindAsync(id);
        if (findAlbum != null)
        {
            musicDbContext.Albums.Remove(findAlbum);
            await musicDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Album>> GetAlbumsByArtist(int? id)
    {
        var albums = await musicDbContext.Albums
            .Where(a => a.ArtistId == id)
            .AsNoTracking()
            .ToListAsync();
        return albums;
    }
}