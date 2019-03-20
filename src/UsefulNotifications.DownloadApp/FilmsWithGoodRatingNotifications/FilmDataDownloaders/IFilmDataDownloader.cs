using System.Collections.Generic;
using System.Threading.Tasks;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public interface IFilmDataDownloader
    {
        Task<IEnumerable<LocationFilmArgs>> DownloadFilmDataAsync(string locationNameOrPostCode);
    }
}