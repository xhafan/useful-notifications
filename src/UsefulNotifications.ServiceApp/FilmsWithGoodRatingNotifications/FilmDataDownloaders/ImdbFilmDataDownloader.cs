using System.Collections.Generic;
using System.Threading.Tasks;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public class ImdbFilmDataDownloader : IFilmDataDownloader // todo: test me
    {
        public async Task<IEnumerable<LocationFilmArgs>> DownloadFilmDataAsync(string locationNameOrPostCode)
        {
            return null;
        }
    }
}