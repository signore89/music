using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Models;
using Music.ViewsModels;
using X.PagedList.Extensions;

namespace Music.Controllers
{
    public class SearchMusic : Controller
    {
        private readonly MusicDbContext _musicDbContext;
        private const int PageSize = 2;

        public SearchMusic(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public IActionResult Index(string searchString, string sortBy,int? page)
        {
            int pageNumber = page ?? 1;
            IQueryable<Song> songsQuery = _musicDbContext.Songs
            .Include(s => s.Album)
            .Include(s => s.Artists);
            // Поиск
            if (!string.IsNullOrEmpty(searchString))
            {
                songsQuery = songsQuery.Where(s =>
                s.Name.Contains(searchString) ||
                s.Album != null && s.Album.Name.Contains(searchString) ||
                s.Artists.Any(a => a.Name.Contains(searchString)));
            }

            // Сортировка
            songsQuery = sortBy switch
            {
                "album" => songsQuery.OrderBy(s => s.Album != null ? s.Album.Name : "").ThenBy(s => s.Name),
                "artist" => songsQuery.OrderBy(s => s.Artists.FirstOrDefault() != null ? s.Artists.FirstOrDefault().Name : "").ThenBy(s => s.Name),
                _ => songsQuery.OrderBy(s => s.Name) // по умолчанию по названию
            };
            //Пагинированный список
            var pagedSongs = songsQuery.ToPagedList(pageNumber, PageSize);

            var viewModel = new SongViewModel
            {
                Songs = pagedSongs,
                SearchTerm = searchString,
                SortBy = sortBy ?? "name"
            };
            return View(viewModel);
        }
    }
}
