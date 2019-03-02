using CoreDdd.Queries;

namespace UsefulNotifications.Queries.FilmsWithGoodRatingNotifications
{
    public class GetLocationDataQuery : IQuery
    {
        public string CountryCode { get; set; }
        public string LocationNameOrPostCode { get; set; }
    }
}