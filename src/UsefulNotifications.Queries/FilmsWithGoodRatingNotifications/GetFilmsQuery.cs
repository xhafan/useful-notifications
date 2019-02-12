using CoreDdd.Queries;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Queries.FilmsWithGoodRatingNotifications
{
    public class GetFilmsQuery : IQuery
    {
        public int CountryId { get; set; }
        public string LocationNameOrPostCode { get; set; }
        public RatingSource RatingSource { get; set; }
        public decimal? MinimalRating { get; set; }
    }
}
