using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications
{
    public class CinemaBuilder
    {
        public const string CinemaName = "cinema name";

        private string _cinemaName = CinemaName;

        public CinemaBuilder WithCinemaName(string cinemaName)
        {
            _cinemaName = cinemaName;
            return this;
        }

        public Cinema Build()
        {
            return new Cinema(_cinemaName);
        }
    }
}