using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class FilmRatingArgs
    {
        public RatingSource Source { get; set; }
        public string Url { get; set; }
        public decimal Rating { get; set; }
    }
}