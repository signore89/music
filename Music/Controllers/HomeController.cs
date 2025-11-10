using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class HomeController(IArtistRepository artistRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Title = "Главная страница";
        var artists = await artistRepository.GetAllAsync();

        return View(artists);
    }
}
