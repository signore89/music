using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class HomeController(IArtistRepository artistRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var artists = await artistRepository.GetAllAsync();

        return View(artists);
    }
}
