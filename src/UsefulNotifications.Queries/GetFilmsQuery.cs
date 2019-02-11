using CoreDdd.Queries;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Queries
{
    public class GetFilmsQuery : IQuery
    {
        public string CountryCode { get; set; }
        public string LocationNameOrPostCode { get; set; }
        public RatingSource RatingSource { get; set; }
        public decimal? MinimalRating { get; set; }
    }
}
