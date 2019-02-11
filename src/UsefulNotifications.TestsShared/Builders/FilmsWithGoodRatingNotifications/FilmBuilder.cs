using System.Collections.Generic;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications
{
    public class FilmBuilder
    {
        public const string FilmName = "film name";
        public const string FilmUrl = "film url";
        public const RatingSource FilmRatingSource = RatingSource.Imdb;
        public const string FilmRating = "8.2";

        private string _filmName = FilmName;
        private string _filmUrl = FilmUrl;
        private IEnumerable<FilmRatingArgs> _filmRatings = new []
        {
            new FilmRatingArgs
            {
                Source = FilmRatingSource,
                Rating = FilmRating,
                Url = FilmUrl
            }
        };

        public FilmBuilder WithFilmName(string filmName)
        {
            _filmName = filmName;
            return this;
        }

        public FilmBuilder WithFilmUrl(string filmUrl)
        {
            _filmUrl = filmUrl;
            return this;
        }

        public FilmBuilder WithFilmRatings(params FilmRatingArgs[] filmRatings)
        {
            _filmRatings = filmRatings;
            return this;
        }

        public Film Build()
        {
            return new Film(_filmName, _filmUrl, _filmRatings);
        }
    }
}
