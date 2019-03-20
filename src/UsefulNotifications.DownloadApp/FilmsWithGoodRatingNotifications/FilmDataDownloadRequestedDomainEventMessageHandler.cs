using System.Threading.Tasks;
using Rebus.Handlers;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders;

namespace UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications
{
    // todo: test me
    public class FilmDataDownloadRequestedDomainEventMessageHandler : IHandleMessages<FilmDataDownloadRequestedDomainEvent>
    {
        private readonly IFilmDataDownloaderFactory _filmDataDownloaderFactory;

        public FilmDataDownloadRequestedDomainEventMessageHandler(
            IFilmDataDownloaderFactory filmDataDownloaderFactory
        )
        {
            _filmDataDownloaderFactory = filmDataDownloaderFactory;
        }

        public async Task Handle(FilmDataDownloadRequestedDomainEvent domainEvent)
        {
            var filmDataDownloader = _filmDataDownloaderFactory.Create(domainEvent.CountryCode);
            var locationFilmArgses = await filmDataDownloader.DownloadFilmDataAsync(domainEvent.LocationNameOrPostCode);

            // todo: publish here downloaded data and handle it in the service app
        }
    }
}