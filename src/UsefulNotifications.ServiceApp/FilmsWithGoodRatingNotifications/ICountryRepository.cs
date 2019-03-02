using System.Threading.Tasks;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications
{
    public interface ICountryRepository
    {
        Task<Country> QueryByCountryCodeAsync(string countryCode);
    }
}