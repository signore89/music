using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;


        public ArtistController(IArtistRepository artist)
        {
            _artistRepository = artist;
        }
        public  IActionResult Index()
        {
            var artist =  _artistRepository.GetAllAsync();
            return View(artist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            return View(artist);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Artist artist)
        {
            var answer = await _artistRepository.EditSave(artist);
            return Ok(answer);// сделать подтверждение
        }

        [HttpPost]
        public async Task<IActionResult> Create(Artist artist)
        {
            await _artistRepository.CreateAsync(artist);
            return RedirectToAction("Index","Home");//indexof
        }

        public  IActionResult GetArtist(int id)
        {
            return View( _artistRepository.GetByIdAsync(id));
        }
        public  IActionResult CreateNewArtist()
        {
            return View();
        }

        public  async Task<IActionResult> DeleteAsync(int id)
        {
            var answer = await _artistRepository.DeleteByIdAsync(id);
            return RedirectToAction("Index", "Home");//indexof
        }
    }
}
