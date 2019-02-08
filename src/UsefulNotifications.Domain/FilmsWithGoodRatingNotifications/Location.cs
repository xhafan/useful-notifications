using System;
using System.Collections.Generic;
using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Location : Entity, IAggregateRoot
    {
        private readonly ICollection<LocationFilm> _films = new HashSet<LocationFilm>();

        protected Location() { }

        public Location(Country country, string nameOrPostCode, IEnumerable<LocationFilmArgs> locationFilmArgses)
        {
            Country = country;
            NameOrPostCode = nameOrPostCode;
            LastUpdated = DateTime.Now;
            foreach (var locationFilmArgs in locationFilmArgses)
            {
                _films.Add(new LocationFilm(locationFilmArgs));
            }
        }

        public virtual Country Country { get; protected set; }
        public virtual string NameOrPostCode { get; protected set; }
        public virtual DateTime LastUpdated { get; protected set; }
        public virtual IEnumerable<LocationFilm> Films => _films;
    }
}