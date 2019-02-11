using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications
{
    public class LocationBuilder
    {
        public const string NameOrPostCode = "location name or post code";

        private string _nameOrPostCode = NameOrPostCode;
        private Country _country;
        private LocationFilmArgs[] _locationFilms;

        public LocationBuilder WithCountry(Country country)
        {
            _country = country;
            return this;
        }

        public LocationBuilder WithNameOrPostCode(string nameOrPostCode)
        {
            _nameOrPostCode = nameOrPostCode;
            return this;
        }

        public LocationBuilder WithLocationFilms(params LocationFilmArgs[] locationFilms)
        {
            _locationFilms = locationFilms;
            return this;
        }

        public Location Build()
        {
            if (_country == null) _country = new CountryBuilder().Build();
            if (_locationFilms == null) _locationFilms = new LocationFilmArgs[0];

            return new Location(_country, _nameOrPostCode, _locationFilms);
        }
    }
}