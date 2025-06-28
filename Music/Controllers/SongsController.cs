using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongRepository _context;
        private readonly IArtistRepository _contextArtist;
        private readonly IAlbumRepository _contextAlbum;

        public SongsController(ISongRepository context, IArtistRepository contextArtist, IAlbumRepository contextAlbum)
        {
            _context = context;
            _contextAlbum = contextAlbum;
            _contextArtist = contextArtist;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var songs = await _context.GetAllAsync();
            return View(songs);
        }
        // GET: Songs
        public async Task<IActionResult> SoundLibraryAlbum(int albumId)
        {
            ViewBag.AlbumId = albumId;
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

        // GET: Songs/Create
        public IActionResult Create(int? idAlbum)
        {
            var song = new Song();
            if (idAlbum != null)
            { 
                song.AlbumId = idAlbum;
                
            }
            else
            {
                SelectList listAlbums = new SelectList(_contextAlbum.GetAllAsync().Result, "Id", "Name");
                ViewBag.Albums = listAlbums;
                SelectList listArtists = new SelectList(_contextArtist.GetAllAsync().Result, "Id", "Name");
                ViewBag.Artists = listArtists;
            }
            
            return View(song);
        }

        // POST: Songs/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Song song)
        {
            var album = await _contextAlbum.GetAlbumByIdAsync(song.AlbumId);
            song.Artists.Add(album.Artist);
            var newSong = await _context.AddNewSongAsync(song);
            return RedirectToAction(nameof(SoundLibraryAlbum), new { albumId = newSong.AlbumId});
        }

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

        // POST: Songs/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Song song, int[] selectedArtists)
        {   
            var songUpdate = await _context.UpdateSongAsync(song,selectedArtists);
            return RedirectToAction(nameof(Index));
        }

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

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idSongDeletedAlbum = await _context.DeleteSongAsync(id);
            return RedirectToAction(nameof(SoundLibraryAlbum), new { albumId = idSongDeletedAlbum });
        }
    }
}
