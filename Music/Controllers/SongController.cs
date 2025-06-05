using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewsModels;
namespace Music.Controllers;

public class SongController(ISongRepository songRepository) : Controller
{
    private const int PageSize = 2;
    public async Task<IActionResult> Index(int page = 1)
    {
        var list = await songRepository.GetAllAsync();
        IQueryable<Song> source = list.AsQueryable();
        var count =  source.Count();
        var items =  source.Skip((page - 1) * PageSize).Take(PageSize).ToList();

        PageViewModel pageViewModel = new PageViewModel(count, page, PageSize);
        IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
        return View(viewModel);
    }

    public async Task<IActionResult> SoundLibrary(int artistId)
    {
        var songs = await songRepository.GetSongsByIdArtistAsync(artistId);
        return View(songs);
    }

    public async Task<IActionResult> FavoriteSongs()
    {
        
        return View(await songRepository.GetFavoriteSongs());
    }

    public async Task<IActionResult> AddFavoriteSongs(int idSong)
    {
        var song = await songRepository.GetDetailsByIdAsync(idSong);
        song.UserId = 1;
        await songRepository.EditSave(song);
        return RedirectToAction("FavoriteSongs");
    }

    public async Task<IActionResult> RemoveFavoriteSongs(int idSong)
    {
        var song = await songRepository.GetDetailsByIdAsync(idSong);
        song.UserId = null;
        await songRepository.EditSave(song);
        return RedirectToAction("FavoriteSongs");
    }
}