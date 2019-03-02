using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Commands.FilmsWithGoodRatingNotifications
{
    public class LocationFilmDataDownloadRequestRepository 
        : NhibernateRepository<LocationFilmDataDownloadRequest>, ILocationFilmDataDownloadRequestRepository // todo: test me
    {
        private readonly NhibernateUnitOfWork _unitOfWork;

        public LocationFilmDataDownloadRequestRepository(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LocationFilmDataDownloadRequest> QueryByCountryCodeAndLocationNameOrPostCodeAsync(string countryCode, string locationNameOrPostCode)
        {
            return await _unitOfWork.Session
                .QueryOver<LocationFilmDataDownloadRequest>()
                .Where(x => x.CountryCode == countryCode && x.LocationNameOrPostCode == locationNameOrPostCode)
                .SingleOrDefaultAsync();
        }
    }
}