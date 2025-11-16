namespace Music.ViewsModels
{
    public class CreatedAlbumViewModels
    {
        public string Name { get; set; } = "unknown album";
        public string YearOfIssue { get; set; } = "2000";
        public IFormFile? File { get; set; }
    }
}
