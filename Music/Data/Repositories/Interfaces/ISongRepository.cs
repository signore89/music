using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        public List <Song> GetSongs();
    }
}
