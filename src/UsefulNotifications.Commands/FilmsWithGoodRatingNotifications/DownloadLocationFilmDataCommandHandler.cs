using System.Threading.Tasks;
using CoreDdd.Commands;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Commands.FilmsWithGoodRatingNotifications
{
    public class DownloadLocationFilmDataCommandHandler : BaseCommandHandler<DownloadLocationFilmDataCommand> // todo: test me
    {
        private readonly ILocationFilmDataDownloadRequestRepository _locationFilmDataDownloadRequestRepository;

        public DownloadLocationFilmDataCommandHandler(
            ILocationFilmDataDownloadRequestRepository locationFilmDataDownloadRequestRepository
        )
        {
            _locationFilmDataDownloadRequestRepository = locationFilmDataDownloadRequestRepository;
        }

        public override async Task ExecuteAsync(DownloadLocationFilmDataCommand command)
        {
            var locationFilmDataDownloadRequest = await _locationFilmDataDownloadRequestRepository
                .QueryByCountryCodeAndLocationNameOrPostCodeAsync(command.CountryCode, command.LocationNameOrPostCode);

            if (locationFilmDataDownloadRequest == null)
            {
                await _createLocationFilmDataDownloadRequest();

                async Task _createLocationFilmDataDownloadRequest()
                {
                    locationFilmDataDownloadRequest =
                        new LocationFilmDataDownloadRequest(command.CountryCode, command.LocationNameOrPostCode, command.RatingSource);
                    await _saveLocationFilmDataDownloadRequestToGenerateItsId();

                    async Task _saveLocationFilmDataDownloadRequestToGenerateItsId()
                    {
                        await _locationFilmDataDownloadRequestRepository.SaveAsync(locationFilmDataDownloadRequest);
                    }
                }
            }

            locationFilmDataDownloadRequest.DownloadFilmData();
        }
    }
}