using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _context;

        public AlbumsController(IAlbumRepository context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var albums = await _context.GetAllAsync();
            return View(albums);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        //GET: Albums/Create
        public IActionResult Create(int id)
        {
            ViewBag.AlbumId = id;
            //ViewData["ArtistId"] ;
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,YearOfIssue,UrlImg,ArtistId")] Album album)
        {
            album.ArtistId = Convert.ToInt32(ViewData["ArtistId"]);
            var id = await _context.AddNewAlbumAsync(album);
            //return RedirectToAction(nameof(Index));
            //ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            //ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,YearOfIssue,UrlImg,ArtistId")] Album album)
        {
            int idNewAlbum = 0;
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                idNewAlbum = await _context.UpdateAlbumAsync(album);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAlbumAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: Albums/id
        public async Task<IActionResult> AlbumsByArtist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albums = await _context.GetAlbumsByArtist(id);
            if (albums == null)
            {
                return NotFound();
            }
            ViewBag.ArtistId = id;
            return View("Index",albums);
        }
    }
}
