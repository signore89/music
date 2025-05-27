using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var albums = albumRepository.GetAll();

        return View(albums);
    }

    public async Task<IActionResult> Details(int id)
    {
        var album = albumRepository.GetDetailsById(id);

        return View(album);
    }
}