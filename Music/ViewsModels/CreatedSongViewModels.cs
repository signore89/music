namespace Music.ViewsModels
{
    public class CreatedSongViewModels
    {
        public string Name { get; set; } = "unknown song";
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public IFormFile? File { get; set; }
    }
}
