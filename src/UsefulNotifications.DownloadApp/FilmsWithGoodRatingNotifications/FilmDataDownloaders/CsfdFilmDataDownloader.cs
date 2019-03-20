using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications.FilmDataDownloaders
{
    public class CsfdFilmDataDownloader : IFilmDataDownloader // todo: test me
    {
        private readonly ICsfdUrlBuilder _csfdUrlBuilder;

        public CsfdFilmDataDownloader(ICsfdUrlBuilder csfdUrlBuilder)
        {
            _csfdUrlBuilder = csfdUrlBuilder;
        }


        public async Task<IEnumerable<LocationFilmArgs>> DownloadFilmDataAsync(string locationNameOrPostCode)
        {
            var csfdLocationUrl = _csfdUrlBuilder.BuildUrl(locationNameOrPostCode);

            var htmlWeb = new HtmlWeb();
            var csfdHtmlDocumentWithListOfCinemasAndFilms = await htmlWeb.LoadFromWebAsync(csfdLocationUrl);

            foreach (var link in csfdHtmlDocumentWithListOfCinemasAndFilms.DocumentNode.SelectNodes("//a[@href]"))
            {

            }

            return null;
        }
    }

    public interface ICsfdUrlBuilder
    {
        string BuildUrl(string locationName);
    }
}