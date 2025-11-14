using Music.Models;
using X.PagedList;

namespace Music.ViewsModels
{
    public class SongViewModel
    {
        public IPagedList<Song> Songs { get; set; }
        public string SearchTerm { get; set; } //поисковый запрос
        public string SortBy { get; set; }
    }
}
