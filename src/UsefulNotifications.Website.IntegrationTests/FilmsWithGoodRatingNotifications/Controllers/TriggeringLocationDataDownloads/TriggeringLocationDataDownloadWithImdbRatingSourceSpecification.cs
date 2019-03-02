using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.TriggeringLocationDataDownloads
{
    public class TriggeringLocationDataDownloadWithImdbRatingSourceSpecification : ITriggeringLocationDataDownloadSpecification
    {
        public SearchForFilmsArgs GetSearchForFilmsArgs()
        {
            return new SearchForFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Imdb,
                ImdbPostCode = "some imdb location",
                ImdbMinimalRating = 8.2m
            };
        }
    }
}