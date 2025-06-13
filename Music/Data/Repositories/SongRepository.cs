using Music.Data.Repositories.Interfaces;
using Music.Models;
using static System.Net.WebRequestMethods;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext musicDbContext) : ISongRepository
    {
        public int AddNewSong(Song song)
        {
            throw new NotImplementedException();
        }

        public int DeleteSong(int id)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetAll()
        {
            throw new NotImplementedException();
        }

        public Song GetSongById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetSongsByArtist(int idArtist, int limit)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetSongsByName(string searchString, int limit)
        {
            throw new NotImplementedException();
        }

        public int UpdateSong(Song song)
        {
            throw new NotImplementedException();
        }
    }
}
