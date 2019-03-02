using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.TriggeringLocationDataDownloads
{
    public class TriggeringLocationDataDownloadWithCsfdRatingSourceSpecification : ITriggeringLocationDataDownloadSpecification
    {
        public SearchForFilmsArgs GetSearchForFilmsArgs()
        {
            return new SearchForFilmsArgs
            {
                CountryCode = "ONE",
                RatingSource = RatingSource.Csfd,
                CsfdLocation = "some csfd location",
                CsfdMinimalRating = 82m
            };
        }
    }
}