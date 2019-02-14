using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.FilmsWithGoodRatingNotificationControllers.SearchingForFilms
{
    public class SearchingForFilmsWithImdbRatingSourceSpecification : ISearchingForFilmsSpecification
    {
        public SearchFilmsArgs GetSearchFilmsArgs()
        {
            return new SearchFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Imdb,
                ImdbPostCode = "location name one",
                ImdbMinimalRating = 8.2m
            };
        }
    }
}