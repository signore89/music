using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Music.Data.Repositories;
using Music.Services.Interfaces;

namespace Music.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly MusicDbContext _context;
        private readonly IMemoryCache _cache;
        private string CacheKeyPrefix; 
        private string GetCacheKey(string userId) => $"{CacheKeyPrefix}{userId}";

        public FavoriteService(MusicDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<bool> AddToFavoriteAlbumAsync(string userId, int albumId)
        {
            if (await IsFavoriteAlbumAsync(userId, albumId))
                return false;

            var user = await _context.Users.FindAsync(userId);
            var album = await _context.Albums.FindAsync(albumId);
            if (user != null)
            {
                user.Albums.Add(album);
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public async Task<bool> AddToFavoriteArtistAsync(string userId, int artistId)
        {
            if (await IsFavoriteArtistAsync(userId, artistId))
                return false;

            var user = await _context.Users.FindAsync(userId);
            var artist = await _context.Artists.FindAsync(artistId);
            if (user != null)
            {
                user.Artists.Add(artist);
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public async Task<bool> AddToFavoriteSongAsync(string userId, int songId)
        {
            if (await IsFavoriteSongAsync(userId, songId))
                return false;

            var user = await _context.Users.FindAsync(userId);
            var song = await _context.Songs.FindAsync(songId);
            if (user != null)
            {
                user.Songs.Add(song);
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                //// Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public async Task<HashSet<int>> GetUserFavoritesAlbumsAsync(string userName)
        {
            var cacheKey = GetCacheKey(userName);
            var favoritesAlbums = await _context.Users
                    .Where(u => u.Id == userName)
                    .Include(u => u.Albums)
                    .SelectMany(u => u.Albums)
                    .Select(a => a.Id)
                    .ToHashSetAsync();

                _cache.Set(cacheKey, favoritesAlbums, TimeSpan.FromMinutes(1));


            return favoritesAlbums;
        }

        public async Task<HashSet<int>> GetUserFavoritesArtistsAsync(string userId)
        {
            var cacheKey = GetCacheKey(userId);
            var favoritesArtists = await _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Artists)
                    .SelectMany(u => u.Artists)
                    .Select(a => a.Id)
                    .ToHashSetAsync();

            _cache.Set(cacheKey, favoritesArtists, TimeSpan.FromMinutes(1));

            return favoritesArtists;
        }

        public async Task<HashSet<int>> GetUserFavoritesSongsAsync(string userId)
        {
            var cacheKey = GetCacheKey(userId);
            var favoritesSongs = await _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Songs)
                    .SelectMany(u => u.Songs)
                    .Select(a => a.Id)
                    .ToHashSetAsync();

                _cache.Set(cacheKey, favoritesSongs, TimeSpan.FromMinutes(1));
            return favoritesSongs;
        }

        public async Task<bool> IsFavoriteAlbumAsync(string userName, int albumId)
        {
            var favorites = await GetUserFavoritesAlbumsAsync(userName);
            return favorites.Contains(albumId);
        }

        public async Task<bool> IsFavoriteArtistAsync(string userId, int artistId)
        {
            var favorites = await GetUserFavoritesArtistsAsync(userId);
            return favorites.Contains(artistId);
        }

        public async Task<bool> IsFavoriteSongAsync(string userId, int songId)
        {
            var favorites = await GetUserFavoritesSongsAsync(userId);
            return favorites.Contains(songId);
        }

        public async Task<bool> RemoveFromFavoriteAlbumAsync(string userId, int albumId)
        {
            var favorite = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Albums)
                .SelectMany(u => u.Albums)
                .FirstOrDefaultAsync(a => a.Id == albumId);

            if (favorite == null)
                return false;

            var user = await _context.Users
                .Include(u => u.Albums)
                .FirstAsync(u => u.Id == userId);
            user.Albums.Remove(favorite);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromFavoriteArtistAsync(string userId, int artistId)
        {
            var favorite = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Artists)
                .SelectMany(u => u.Artists)
                .FirstOrDefaultAsync(a => a.Id == artistId);

            if (favorite == null)
                return false;

            var user = await _context.Users
                .Include(u => u.Artists)
                .FirstAsync(u => u.Id == userId);
            user.Artists.Remove(favorite);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromFavoriteSongAsync(string userId, int songId)
        {
            var favorite = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Songs)
                .SelectMany(u => u.Songs)
                .FirstOrDefaultAsync(a => a.Id == songId);

            if (favorite == null)
                return false;

            var user = await _context.Users
                .Include(u => u.Songs)
                .FirstAsync(u => u.Id == userId);
            user.Songs.Remove(favorite);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Инвалидируем кеш
                _cache.Remove(GetCacheKey(userId));
                return true;
            }

            return false;
        }

        public void AddCacheKeyPrefix(string prefix)
        {
            CacheKeyPrefix = prefix;
        }
    }
}
