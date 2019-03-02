using System.Threading.Tasks;
using CoreDdd.Nhibernate.UnitOfWorks;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications
{
    public class CountryRepository : ICountryRepository // todo: test me
    {
        private readonly NhibernateUnitOfWork _unitOfWork;

        public CountryRepository(NhibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Country> QueryByCountryCodeAsync(string countryCode)
        {
            return await _unitOfWork.Session
                .QueryOver<Country>()
                .Where(x => x.Code == countryCode)
                .SingleOrDefaultAsync();
        }
    }
}