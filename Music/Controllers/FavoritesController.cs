using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Services.Interfaces;

namespace Music.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IUserProvider _userProvider;

        public FavoritesController(IFavoriteService favoriteService, IUserProvider userProvider)
        {
            _favoriteService = favoriteService;
            _userProvider = userProvider;
        }

        [HttpPost]
        public async Task<IActionResult> AddAlbums(int albumId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.AddToFavoriteAlbumAsync(userId, albumId);
            return Json(new { success = result });
        }
        [HttpPost]
        public async Task<IActionResult> AddArtist(int artistId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.AddToFavoriteArtistAsync(userId, artistId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(int songId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.AddToFavoriteSongAsync(userId, songId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAlbum(int albumId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.RemoveFromFavoriteAlbumAsync(userId, albumId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveArtist(int artistId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.RemoveFromFavoriteArtistAsync(userId, artistId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSong(int songId)
        {
            var userId = _userProvider.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _favoriteService.RemoveFromFavoriteSongAsync(userId, songId);
            return Json(new { success = result });
        }
    }
}
