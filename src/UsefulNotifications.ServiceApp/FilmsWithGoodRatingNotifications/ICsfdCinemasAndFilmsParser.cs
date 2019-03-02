using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications
{
    public interface ICsfdCinemasAndFilmsParser
    {
        LocationFilmArgs ParseCinemasAndFilms();
    }
}