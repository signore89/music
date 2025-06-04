using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class SongController(ISongRepository songRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var songs = await songRepository.GetAllAsync();

        return View(songs);
    }

    public async Task<IActionResult> SoundLibrary(int artistId)
    {
        var songs = await songRepository.GetSongsByIdArtistAsync(artistId);
        return View(songs);
    }
}