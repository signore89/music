using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var AlbumsJson = TempData["Albums"] as string;
        if (AlbumsJson.IsNullOrEmpty())
        {
            var albums = await albumRepository.GetAllAsync();
            return View(albums);
        }
        var albumsS = JsonSerializer.Deserialize<List<Album>>(AlbumsJson);
        return View(albumsS);      
    }

    public async Task<IActionResult> Details(int id, string name)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);

        return View(album);
    }

    public async Task<IActionResult> GetArtistAlbums(int artistId)
    {
        var listAlbums = await albumRepository.SearchArtistAlbums(artistId);
        TempData["Albums"] = JsonSerializer.Serialize(listAlbums);
        return RedirectToAction("Index");
    }
}