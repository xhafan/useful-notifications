namespace UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public class FilmDataDownloaderFactory : IFilmDataDownloaderFactory
    {
        private readonly ICsfdUrlBuilder _csfdUrlBuilder;

        public FilmDataDownloaderFactory(ICsfdUrlBuilder csfdUrlBuilder)
        {
            _csfdUrlBuilder = csfdUrlBuilder;
        }

        public IFilmDataDownloader Create(string countryCode)
        {
            return countryCode == "CZ" || countryCode == "SK"
                ? (IFilmDataDownloader)new CsfdFilmDataDownloader(_csfdUrlBuilder)
                : new ImdbFilmDataDownloader();
        }
    }
}