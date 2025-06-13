using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository(MusicDbContext musicDbContext) : IArtistRepository
    {
        public int AddNewArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public int DeleteArtist(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artist> GetAll()
        {
            throw new NotImplementedException();
        }

        public Artist GetArtistById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artist> GetArtistsByName(string searchString, int limit)
        {
            throw new NotImplementedException();
        }

        public int UpdateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
