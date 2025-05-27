using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        public List<Artist> GetArtists()
        {
            return new List<Artist>
            {
                new Artist
                {
                     Id = 0,
                     Name = "Артист1",
                     UrlImg = "https://avatars.mds.yandex.net/get-entity_search/5735732/1132006118/SUx182"
                }
            };
        }
    }
}
