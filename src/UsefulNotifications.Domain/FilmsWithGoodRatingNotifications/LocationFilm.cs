using System.Collections.Generic;
using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilm : Entity
    {
        private readonly ICollection<LocationFilmCinema> _cinemas = new HashSet<LocationFilmCinema>();

        protected LocationFilm() {}

        public LocationFilm(Location location, LocationFilmArgs locationFilmArgs)
        {
            Location = location;
            Film = locationFilmArgs.Film;
            foreach (var locationFilmCinemaArgs in locationFilmArgs.Cinemas)
            {
                _cinemas.Add(new LocationFilmCinema(this, locationFilmCinemaArgs));
            }
        }

        public virtual Location Location { get; protected set; }
        public virtual Film Film { get; protected set; }
        public virtual IEnumerable<LocationFilmCinema> Cinemas => _cinemas;
    }
}