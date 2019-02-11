using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications
{
    public class CountryBuilder
    {
        public const string CountryName = "country name";
        public const string CountryCode = "country code";

        private string _countryName = CountryName;
        private string _countryCode = CountryCode;

        public CountryBuilder WithCountryName(string countryName)
        {
            _countryName = countryName;
            return this;
        }

        public CountryBuilder WithCountryCode(string countryCode)
        {
            _countryCode = countryCode;
            return this;
        }

        public Country Build()
        {
            return new Country(_countryName, _countryCode);
        }
    }
}