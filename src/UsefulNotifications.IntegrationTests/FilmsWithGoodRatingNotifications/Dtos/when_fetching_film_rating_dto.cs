using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.IntegrationTests.FilmsWithGoodRatingNotifications.Dtos
{
    [TestFixture]
    public class when_fetching_film_rating_dto : BaseIntegrationTest
    {
        private FilmRatingDto _filmRatingDto;
        private Film _film;

        [SetUp]
        public void Context()
        {
            var country = new CountryBuilder().Build();
            UnitOfWork.Save(country);

            var cinema = new CinemaBuilder().Build();
            UnitOfWork.Save(cinema);

            _film = new FilmBuilder().Build();
            UnitOfWork.Save(_film);

            var location = new LocationBuilder()
                .WithCountry(country)
                .WithLocationFilms(new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new[]
                    {
                        new LocationFilmCinemaArgs { Cinema = cinema }
                    }
                })
                .Build();
            UnitOfWork.Save(location);

            UnitOfWork.Clear();

            var locationFilmId = location.Films.Single().Id;
            _filmRatingDto = UnitOfWork.Session.QueryOver<LocationFilmDto>()
                .Where(x => x.Id == locationFilmId)
                .List().Single()
                .Ratings.SingleOrDefault();
        }

        [Test]
        public void film_rating_dto_contains_correct_data()
        {
            _filmRatingDto.ShouldNotBeNull();
            _filmRatingDto.Id.ShouldBe(_film.FilmRatings.Single().Id);
            _filmRatingDto.RatingSource.ShouldBe(FilmBuilder.FilmRatingSource);
            _filmRatingDto.FilmUrl.ShouldBe(FilmBuilder.FilmUrl);
            _filmRatingDto.Rating.ShouldBe(FilmBuilder.FilmRating);
        }
    }
}