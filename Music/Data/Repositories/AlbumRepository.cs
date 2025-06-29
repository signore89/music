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
        return await musicDbContext.Albums
            .Include(a => a.Artist)
            .FirstAsync(a => a.Id == id);
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

    public async Task<List<Album>> GetAlbumsByNameAsync(string searchString)
    {

        var albums = await musicDbContext.Albums
            .Where(a => a.Name.Contains(searchString))
            .AsNoTracking()
            .ToListAsync();
        return albums;
    }

    public async Task<Album> AddNewAlbumAsync(Album album)
    {
        var myObject = await musicDbContext.Albums.AddAsync(album);
        await musicDbContext.SaveChangesAsync();
        return myObject.Entity;
    }

    public async Task<Album> UpdateAlbumAsync(Album album)
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
            return existingAlbum;
        } 
        else
        {
            return null;//                                                    ДЕЛЬНЫЙ ВОЗВРАТ НУЖЕН
        }
    }

    public async Task<int> DeleteAlbumAsync(int id)
    {
        var findAlbum = await musicDbContext.Albums.FindAsync(id);
        if (findAlbum != null)
        {
            musicDbContext.Albums.Remove(findAlbum);
            await musicDbContext.SaveChangesAsync();
            return findAlbum.ArtistId;
        }
        return 0;
    }

    public async Task<List<Album>> GetAlbumsByArtist(int? id, int quantity, int take)
    {
        var albums = await musicDbContext.Albums
            .Where(a => a.ArtistId == id)
            .Skip(quantity)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();
        return albums;
    }

    public async Task<IEnumerable<Album>> GetPaginationAsync(int quantity, int take)
    {
        var albums = await musicDbContext.Albums.Skip(quantity).Take(take).AsNoTracking().ToListAsync();
        return albums;
    }

    public async Task<int> GetQuantity()
    {
        var temp = await musicDbContext.Albums.CountAsync();
        return temp;
    }
}