using System.Collections.Generic;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class FilmViewModel
    {
        public string FilmName { get; set; }
        public string FilmMainUrl { get; set; }
        public IEnumerable<RatingViewModel> Ratings { get; set; }
        public IEnumerable<string> Cinemas { get; set; }
    }
}