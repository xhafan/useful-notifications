using System.Threading.Tasks;
using CoreDdd.Domain.Repositories;
using Rebus.Handlers;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications
{
    // todo: test me
    // todo: this should not start a transaction until all data are downloaded
    public class FilmDataDownloadRequestedDomainEventMessageHandler : IHandleMessages<FilmDataDownloadRequestedDomainEvent>
    {
        private readonly IFilmDataDownloaderFactory _filmDataDownloaderFactory;
        private readonly ICountryRepository _countryRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<LocationFilmDataDownloadRequest> _locationFilmDataDownloadRequestRepository;

        public FilmDataDownloadRequestedDomainEventMessageHandler(
            IFilmDataDownloaderFactory filmDataDownloaderFactory,
            ICountryRepository countryRepository,
            IRepository<Location> locationRepository,
            IRepository<LocationFilmDataDownloadRequest> locationFilmDataDownloadRequestRepository
        )
        {
            _locationFilmDataDownloadRequestRepository = locationFilmDataDownloadRequestRepository;
            _filmDataDownloaderFactory = filmDataDownloaderFactory;
            _countryRepository = countryRepository;
            _locationRepository = locationRepository;
        }

        public async Task Handle(FilmDataDownloadRequestedDomainEvent domainEvent)
        {
            var filmDataDownloader = _filmDataDownloaderFactory.Create(domainEvent.CountryCode);
            var locationFilmArgses = await filmDataDownloader.DownloadFilmDataAsync(domainEvent.LocationNameOrPostCode);

            var country = await _countryRepository.QueryByCountryCodeAsync(domainEvent.CountryCode);
            var location = new Location(country, domainEvent.LocationNameOrPostCode, locationFilmArgses);
            await _locationRepository.SaveAsync(location);

            var locationFilmDataDownloadRequest = await _locationFilmDataDownloadRequestRepository
                .GetAsync(domainEvent.LocationFilmDataDownloadRequestId);

            locationFilmDataDownloadRequest.OnDownloadCompleted(location);
        }
    }
}