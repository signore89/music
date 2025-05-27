using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        public List<Artist> GetArtists();
    }
}
