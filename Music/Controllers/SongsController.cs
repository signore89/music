using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.Services.Interfaces;
using Music.ViewsModels;
using Uploadcare;
using Uploadcare.Upload;

namespace Music.Controllers
{
    [Authorize]
    public class SongsController : Controller
    {
        private readonly ISongRepository _context;
        private readonly IArtistRepository _contextArtist;
        private readonly IAlbumRepository _contextAlbum;
        private readonly IFavoriteService _favoriteService;
        private readonly IUserProvider _userProvider;
        private readonly UploadcareClient _uploadcareClient;
        private readonly string prefixKey = "Songs";

        public SongsController(ISongRepository context, IArtistRepository contextArtist
            , IAlbumRepository contextAlbum, IFavoriteService favoriteService, IUserProvider userProvider)
        {
            _context = context;
            _contextAlbum = contextAlbum;
            _contextArtist = contextArtist;
            _favoriteService = favoriteService;
            _favoriteService.AddCacheKeyPrefix(prefixKey);
            _userProvider = userProvider;
            _uploadcareClient = new("9fd34966fc25c4304cbd", "9da5be88a9144fed0788");
        }

        const int pageSize = 2;

        // GET: Songs
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
            ViewBag.UserFavoritesSongs = await _favoriteService
                .GetUserFavoritesSongsAsync(_userProvider.GetCurrentUserId());
            var count = await _context.GetQuantity();
            var pager = new PageViewModel(count, page);
            var skip = (page - 1) * pageSize;
            var songs = await _context.GetPaginationAsync(skip, pager.PageSize);
            ViewBag.Pager = pager;
            return View(songs);
        }
        // GET: Songs
        public async Task<IActionResult> SoundLibraryAlbum(int albumId)
        {
            var nameAlbum = await _contextAlbum.GetAlbumByIdAsync(albumId);
            ViewBag.Album = nameAlbum.Name;
            ViewBag.AlbumId = albumId;
            ViewBag.UserFavoritesSongs = await _favoriteService
                .GetUserFavoritesSongsAsync(_userProvider.GetCurrentUserId());
            var songs = await _context.GetSongByAlbum(albumId);
            return View(songs);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Songs/Create
        public IActionResult Create(int? idAlbum)
        {
            var songViewModel = new CreatedSongViewModels();
            if (idAlbum.HasValue)
            { 
                songViewModel.AlbumId = idAlbum.Value;
                
            }
            else
            {
                SelectList listAlbums = new SelectList(_contextAlbum.GetAllAsync().Result, "Id", "Name");
                ViewBag.Albums = listAlbums;
                SelectList listArtists = new SelectList(_contextArtist.GetAllAsync().Result, "Id", "Name");
                ViewBag.Artists = listArtists;
            }
            
            return View(songViewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Songs/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreatedSongViewModels createdSongViewModels)
        {
            using var memoryStream = new MemoryStream();
            await createdSongViewModels.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var fileUploader = new FileUploader(_uploadcareClient);
            var result = await fileUploader.Upload(fileBytes, createdSongViewModels.File.FileName);
            
            if (ModelState.IsValid)
            {
                var album = await _contextAlbum.GetAlbumByIdAsync(createdSongViewModels.AlbumId);
                var artist = await _contextArtist.GetArtistByIdAsync(album.ArtistId);
                var newSong = new Song
                {
                    AlbumId = createdSongViewModels.AlbumId,
                    Album = album,
                    Name = createdSongViewModels.Name,
                    UrlSong = result.OriginalFileUrl
                };
                newSong.Artists.Add(album.Artist);
                var idNewSong = await _context.AddNewSongAsync(newSong);
                return RedirectToAction(nameof(Details), new { id = idNewSong.Id });
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            SelectList listAlbums = new SelectList(await _contextAlbum.GetAllAsync(), "Id", "Name");
            ViewBag.Albums = listAlbums;
            //SelectList listArtists = new SelectList(await _contextArtist.GetAllAsync(), "Id", "Name"); 
            ViewBag.Artists = await _contextArtist.GetAllAsync();
            return View(song);
        }

        [Authorize(Roles = "Admin")]
        // POST: Songs/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Song song, int[] selectedArtists)
        {   
            var songUpdate = await _context.UpdateSongAsync(song,selectedArtists);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.GetSongByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [Authorize(Roles = "Admin")]
        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idSongDeletedAlbum = await _context.DeleteSongAsync(id);
            return RedirectToAction(nameof(SoundLibraryAlbum), new { albumId = idSongDeletedAlbum });
        }
    }
}
