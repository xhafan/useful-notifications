using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Dtos.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_fetching_location_film_dto : BaseIntegrationTest
    {
        private LocationFilmDto _locationFilmDto;
        private Film _film;
        private Location _location;

        [SetUp]
        public void Context()
        {
            var country = new CountryBuilder().Build();
            UnitOfWork.Save(country);

            var cinema = new CinemaBuilder().Build();
            UnitOfWork.Save(cinema);

            _film = new FilmBuilder().Build();
            UnitOfWork.Save(_film);

            _location = new LocationBuilder()
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
            UnitOfWork.Save(_location);

            UnitOfWork.Clear();

            _locationFilmDto = UnitOfWork.Session.QueryOver<LocationFilmDto>()
                .Where(x => x.LocationId == _location.Id)
                .List().SingleOrDefault();
        }

        [Test]
        public void location_film_dto_contains_correct_data()
        {
            _locationFilmDto.ShouldNotBeNull();
            _locationFilmDto.Id.ShouldBe(_location.Films.Single().Id);
            _locationFilmDto.FilmName.ShouldBe(_film.Name);
            _locationFilmDto.FilmMainUrl.ShouldBe(_film.MainUrl);
        }
    }
}