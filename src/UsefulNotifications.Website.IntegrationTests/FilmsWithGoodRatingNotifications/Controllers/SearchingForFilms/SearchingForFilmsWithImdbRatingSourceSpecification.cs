using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.SearchingForFilms
{
    public class SearchingForFilmsWithImdbRatingSourceSpecification : ISearchingForFilmsSpecification
    {
        public SearchForFilmsArgs GetSearchForFilmsArgs()
        {
            return new SearchForFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Imdb,
                ImdbPostCode = "location name one",
                ImdbMinimalRating = 8.2m
            };
        }
    }
}