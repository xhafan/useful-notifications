using System.Collections.Generic;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilmArgs
    {
        public Film Film { get; set; }
        public IEnumerable<LocationFilmCinemaArgs> Cinemas { get; set; }
    }
}