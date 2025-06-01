using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Models;

namespace Music.Controllers
{
    public class ArtistAddController : Controller
    {
        private readonly MusicDbContext _musicDbContext;
        public ArtistAddController(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Artist artist)
        {
            _musicDbContext.Artists.Add(artist);
            await _musicDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
