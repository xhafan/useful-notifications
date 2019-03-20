namespace UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public interface IFilmDataDownloaderFactory
    {
        IFilmDataDownloader Create(string countryCode);
    }
}