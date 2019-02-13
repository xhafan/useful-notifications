using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilmCinema : Entity
    {
        protected LocationFilmCinema() {}

        public LocationFilmCinema(LocationFilm locationFilm, LocationFilmCinemaArgs locationFilmCinemaArgs)
        {
            LocationFilm = locationFilm;
            Cinema = locationFilmCinemaArgs.Cinema;
        }

        public virtual LocationFilm LocationFilm { get; protected set; }
        public virtual Cinema Cinema { get; protected set; }
    }
}