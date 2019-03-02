using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreDdd.Domain;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Location : Entity, IAggregateRoot
    {
        private readonly ICollection<LocationFilm> _films = new HashSet<LocationFilm>();

        protected Location() { }

        public Location(Country country, string nameOrPostCode, IEnumerable<LocationFilmArgs> locationFilmArgses) // todo: locationFilmArgses likely won't be used in the real app
        {
            Country = country;
            NameOrPostCode = nameOrPostCode;
            LastUpdated = DateTime.Now;
            foreach (var locationFilmArgs in locationFilmArgses)
            {
                _films.Add(new LocationFilm(this, locationFilmArgs));
            }
        }

        public virtual Country Country { get; protected set; }
        public virtual string NameOrPostCode { get; protected set; }
        public virtual DateTime LastUpdated { get; protected set; }
        public virtual IEnumerable<LocationFilm> Films => _films;

//        public virtual async Task DownloadData(RatingSource ratingSource, IFilmDataDownloader filmDataDownloader) // todo: test me
//        {
//            if (!_isExpired()) return;
//
//            filmDataDownloader.DownloadData()
//        }

        public virtual bool IsExpired() // todo: test me
        {
            return (DateTime.Now - LastUpdated).Days > 1;  // todo: configure this; for now data older than 1 days are considered expired
        }
    }
}