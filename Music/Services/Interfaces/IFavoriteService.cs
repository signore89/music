namespace Music.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoriteAlbumAsync(string userId, int albumId);
        Task<bool> AddToFavoriteArtistAsync(string userId, int artistId);
        Task<bool> AddToFavoriteSongAsync(string userId, int songId);
        Task<bool> RemoveFromFavoriteAlbumAsync(string userId, int albumId);
        Task<bool> RemoveFromFavoriteArtistAsync(string userId, int artistId);
        Task<bool> RemoveFromFavoriteSongAsync(string userId, int songId);
        Task<bool> IsFavoriteAlbumAsync(string userName, int albumId);
        Task<bool> IsFavoriteArtistAsync(string userId, int artistId);
        Task<bool> IsFavoriteSongAsync(string userId, int songId);
        Task<HashSet<int>> GetUserFavoritesArtistsAsync(string userName);
        Task<HashSet<int>> GetUserFavoritesAlbumsAsync(string userId);
        Task<HashSet<int>> GetUserFavoritesSongsAsync(string userId);
        void AddCacheKeyPrefix(string prefix);
    }
}
