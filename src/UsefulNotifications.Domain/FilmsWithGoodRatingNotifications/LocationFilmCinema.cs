using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilmCinema : Entity
    {
        protected LocationFilmCinema() {}

        public LocationFilmCinema(LocationFilmCinemaArgs locationFilmCinemaArgs)
        {
            Cinema = locationFilmCinemaArgs.Cinema;
        }

        public virtual Cinema Cinema { get; protected set; }
    }
}