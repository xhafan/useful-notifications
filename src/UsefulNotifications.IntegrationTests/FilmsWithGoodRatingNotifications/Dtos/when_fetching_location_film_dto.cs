using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.IntegrationTests.FilmsWithGoodRatingNotifications.Dtos
{
    [TestFixture]
    public class when_querying_films : BaseIntegrationTest
    {
        private LocationFilmDto _locationFilmDto;
        private Film _film;
        private Location _location;
        private Country _country;

        [SetUp]
        public void Context()
        {
            _country = new CountryBuilder().Build();
            UnitOfWork.Save(_country);

            var cinema = new CinemaBuilder().Build();
            UnitOfWork.Save(cinema);

            _film = new FilmBuilder()
                .WithFilmRatings(
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Imdb,
                        Rating = 8.2m,
                        Url = "imdb url"
                    },
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Csfd,
                        Rating = 82m,
                        Url = "csfd url"
                    }
                )
                .Build();
            UnitOfWork.Save(_film);

            _location = new LocationBuilder()
                .WithCountry(_country)
                .WithNameOrPostCode("location name or post code")
                .WithLocationFilms(new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new[]
                    {
                        new LocationFilmCinemaArgs { Cinema = cinema }
                    }
                })
                .Build();
            UnitOfWork.Save(_location);

            UnitOfWork.Clear();

            var locationFilmId = _location.Films.Single().Id;
            _locationFilmDto = UnitOfWork.Session.QueryOver<LocationFilmDto>()
                .Where(x => x.Id == locationFilmId)
                .List().SingleOrDefault();
        }

        [Test]
        public void location_film_dto_contains_correct_data()
        {
            _locationFilmDto.ShouldNotBeNull();
            _locationFilmDto.CountryId.ShouldBe(_country.Id);
            _locationFilmDto.LocationNameOrPostCode.ShouldBe("location name or post code");
            _locationFilmDto.FilmName.ShouldBe(_film.Name);
            _locationFilmDto.FilmMainUrl.ShouldBe(_film.MainUrl);
        }
    }
}