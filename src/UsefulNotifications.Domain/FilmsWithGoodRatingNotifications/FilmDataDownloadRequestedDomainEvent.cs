using CoreDdd.Domain.Events;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class FilmDataDownloadRequestedDomainEvent : IDomainEvent
    {
        public int LocationFilmDataDownloadRequestId { get; set; }
        public string CountryCode { get; set; }
        public string LocationNameOrPostCode { get; set; }
        public RatingSource RatingSource { get; set; }
    }
}