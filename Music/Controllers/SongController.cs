using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class SongController(ISongRepository songRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var album = songRepository.GetSongs();

        return View(album);
    }
}