using Music.Data.Repositories.Interfaces;
using Music.Models;
using static System.Net.WebRequestMethods;

namespace Music.Data.Repositories
{
    public class SongRepository : ISongRepository
    {
        public List<Song> GetSongs()
        {
            return new List<Song>
            {
                new Song
                {
                     Id = 0,
                     Name = "Трек1",
                     UrlSong = "https://rus.hitmotop.com/song/47851939"
                }

            };
        }
    }
}
