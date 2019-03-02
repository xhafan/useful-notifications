using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.TriggeringLocationDataDownloads
{
    public interface ITriggeringLocationDataDownloadSpecification
    {
        SearchForFilmsArgs GetSearchForFilmsArgs();
    }
}