using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _context;

        public ArtistsController(IArtistRepository context) // ВОЗМОЖНО ПРЕЙДЕТЬСЯ ЗАРЕГИИСТРИРОВАТЬ КАК СИНГЛТОН 
        {
            _context = context;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            var artists = await _context.GetAllAsync();
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

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

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
            return View(artist);
        }

        // POST: Artists/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UrlImg")] Artist artist)
        {
            int idNewArtist = 0;
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                idNewArtist = await _context.UpdateArtistAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(await _context.GetArtistByIdAsync(idNewArtist));
        }

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

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        public  async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.DeleteArtist(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
