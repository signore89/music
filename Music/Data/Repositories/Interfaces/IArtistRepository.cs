using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        public List<Artist> GetAll();// мне кажется он не нужен
        public Artist GetArtistById(int id);
        public List<Artist> GetArtistsByName(string searchString, int limit);
        public int AddNewArtist(Artist artist);
        public int UpdateArtist(Artist artist);
        public int DeleteArtist(int id);
    }
}
