using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        public List<Song> GetAll();// мне кажется он не нужен
        public Song GetSongById(int id);
        public List<Song> GetSongsByArtist(int idArtist, int limit);// возможно по имени артиста нужно искать
        public List<Song> GetSongsByName(string searchString, int limit);
        public int AddNewSong(Song song);
        public int UpdateSong(Song song);
        public int DeleteSong(int id);
    }
}
