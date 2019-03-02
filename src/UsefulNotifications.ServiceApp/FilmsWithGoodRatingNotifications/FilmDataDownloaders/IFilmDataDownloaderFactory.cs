namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public interface IFilmDataDownloaderFactory
    {
        IFilmDataDownloader Create(string countryCode);
    }
}