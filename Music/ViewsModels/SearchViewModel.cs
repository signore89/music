using Music.Models;

namespace Music.ViewsModels
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public string SearchType { get; set; } // "albums" или "artists"
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Song> Songs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
