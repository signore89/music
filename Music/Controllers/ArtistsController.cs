using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewsModels;

namespace Music.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _context;
        private readonly ISongRepository _contextSong;
        private readonly IAlbumRepository _contextAlbum;

        public ArtistsController(IArtistRepository context, ISongRepository contextSong, IAlbumRepository contextAlbum)
        {
            _context = context;
            _contextAlbum = contextAlbum;
            _contextSong = contextSong;
        }

        const int pageSize = 2;


        // GET: Artists
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
            var count = await _context.GetQuantity();
            var pager = new PageViewModel(count, page);
            var skip = (page - 1) * pageSize;
            var artists = await _context.GetPaginationAsync(skip,pager.PageSize);
            ViewBag.Pager = pager;
            return View(artists);
        }
        //// GET: AllArtistsByAlbum                              ПАГИНАЦИЯ
        //public async Task<IActionResult> GetAllArtistsByAlbum(int idAlbum)
        //{
        //    var artists = await _context.GetAllArtistByAlbumAsync(idAlbum);
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Artists/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,UrlImg")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                var idNewArtist = await _context.AddNewArtistAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = await _context.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewBag.Albums = await _contextAlbum.GetAllAsync();
            ViewBag.Songs = await _contextSong.GetAllAsync();
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // POST: Artists/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit (Artist artist, int[] selectedSongs, int[] selectedAlbums)
        {
            var updateArtist = await _context.UpdateArtistAsync(artist,selectedSongs,selectedAlbums);
            return RedirectToAction(nameof(Details), new { id = updateArtist.Id });
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        public  async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.DeleteArtist(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
