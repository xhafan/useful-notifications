using System.Threading.Tasks;
using CoreDdd.Domain.Repositories;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Commands.FilmsWithGoodRatingNotifications
{
    public interface ILocationFilmDataDownloadRequestRepository : IRepository<LocationFilmDataDownloadRequest>
    {
        Task<LocationFilmDataDownloadRequest> QueryByCountryCodeAndLocationNameOrPostCodeAsync(string countryCode, string locationNameOrPostCode);
    }
}