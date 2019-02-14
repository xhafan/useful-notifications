using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.FilmsWithGoodRatingNotificationControllers.SearchingForFilms
{
    public class SearchingForFilmsWithCsfdRatingSourceSpecification : ISearchingForFilmsSpecification
    {
        public SearchFilmsArgs GetSearchFilmsArgs()
        {
            return new SearchFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Csfd,
                CsfdLocation = "location name one",
                CsfdMinimalRating = 82m
            };
        }
    }
}