using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.ViewsModels;

namespace Music.Controllers
{
    public class SearchController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private const int PageSize = 2;
        public SearchController(IArtistRepository artistRepository, IAlbumRepository albumRepository, ISongRepository songRepository)
        {
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _songRepository = songRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string query, string searchType = "albums", int page = 1)
        {
            var viewModel = new SearchViewModel
            {
                Query = query,
                SearchType = searchType,
                CurrentPage = page
            };

            if (string.IsNullOrEmpty(query))
            {
                return View(viewModel);
            }

            if (searchType == "albums")
            {
                var albumsQuery = await _albumRepository.GetAlbumsByNameAsync(query);

                viewModel.TotalItems = albumsQuery.Count();
                viewModel.TotalPages = (int)Math.Ceiling(viewModel.TotalItems / (double)PageSize);

                viewModel.Albums = albumsQuery
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
            else if (searchType == "artists")
            {
                var artistsQuery = await _artistRepository.GetArtistsByNameAsync(query);
                viewModel.TotalItems = artistsQuery.Count();
                viewModel.TotalPages = (int)Math.Ceiling(viewModel.TotalItems / (double)PageSize);

                viewModel.Artists = artistsQuery
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
            else
            {
                var songsQuery = await _songRepository.GetSongsByNameAsync(query);
                viewModel.TotalItems = songsQuery.Count();
                viewModel.TotalPages = (int)Math.Ceiling(viewModel.TotalItems / (double)PageSize);

                viewModel.Songs = songsQuery
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }

            return View(viewModel);
        }
    }
}
