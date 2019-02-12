using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications
{
    public class FilmRatingDto
    {
        public int Id { get; set; }
        public RatingSource RatingSource { get; set; }
        public string FilmUrl { get; set; }
        public decimal Rating { get; set; }
    }
}