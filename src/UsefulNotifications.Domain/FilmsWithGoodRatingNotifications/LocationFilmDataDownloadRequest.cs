using System;
using CoreDdd.Domain;
using CoreDdd.Domain.Events;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilmDataDownloadRequest : Entity, IAggregateRoot // todo: add persistence test
    {
        protected LocationFilmDataDownloadRequest() { }

        public LocationFilmDataDownloadRequest( // todo: test me
            string countryCode, 
            string locationNameOrPostCode,
            RatingSource ratingSource
            ) 
        {
            CountryCode = countryCode;
            LocationNameOrPostCode = locationNameOrPostCode;
            RatingSource = ratingSource;
        }

        public virtual string CountryCode { get; protected set; }
        public virtual string LocationNameOrPostCode { get; protected set; }
        public virtual RatingSource RatingSource { get; protected set; }

        public virtual DateTime DownloadRequestedOn { get; protected set; }
        public virtual bool HasCurrentDownloadBeenCompleted { get; protected set; }
        public virtual Location Location { get; protected set; }

        public virtual void DownloadFilmData() // todo: test me
        {
            _CheckThatIdHasBeenGenerated();

            if (_IsItLessThanOneDaySinceLastDownloadRequest()) return;
            if (!_IsFilmDataExpired()) return;

            DownloadRequestedOn = DateTime.Now;
            HasCurrentDownloadBeenCompleted = false;

            DomainEvents.RaiseEvent(new FilmDataDownloadRequestedDomainEvent
            {
                LocationFilmDataDownloadRequestId = Id,
                CountryCode = CountryCode,
                LocationNameOrPostCode = LocationNameOrPostCode,
                RatingSource = RatingSource
            });
        }

        private bool _IsItLessThanOneDaySinceLastDownloadRequest()
        {
            return (DateTime.Now - DownloadRequestedOn).TotalDays < 1;
        }

        private void _CheckThatIdHasBeenGenerated()
        {
            if (Id == default(int))
            {
                throw new Exception(
                    "Id has not been assigned yet - entity creation has not been completed yet. Save the entity first to generate its Id");
            }
        }

        private bool _IsFilmDataExpired() // todo: test me
        {
            return Location == null || (DateTime.Now - Location.LastUpdated).TotalDays > 1;  // todo: configure this; for now data older than 1 days are considered expired
        }

        public virtual void OnDownloadCompleted(Location location) // todo: test me
        {
            Location = location;
            HasCurrentDownloadBeenCompleted = true;
        }
    }
}