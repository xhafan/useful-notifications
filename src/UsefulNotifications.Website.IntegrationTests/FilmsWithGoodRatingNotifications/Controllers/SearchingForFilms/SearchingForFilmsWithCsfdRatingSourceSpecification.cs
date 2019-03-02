using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.SearchingForFilms
{
    public class SearchingForFilmsWithCsfdRatingSourceSpecification : ISearchingForFilmsSpecification
    {
        public SearchForFilmsArgs GetSearchForFilmsArgs()
        {
            return new SearchForFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Csfd,
                CsfdLocation = "location name one",
                CsfdMinimalRating = 82m
            };
        }
    }
}