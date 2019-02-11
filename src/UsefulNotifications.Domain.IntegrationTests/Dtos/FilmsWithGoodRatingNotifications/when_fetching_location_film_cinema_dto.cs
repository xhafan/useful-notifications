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
    public class when_fetching_location_film_cinema_dto : BaseIntegrationTest
    {
        private LocationFilmCinemaDto _locationFilmCinemaDto;
        private Film _film;
        private Location _location;
        private Cinema _cinema;

        [SetUp]
        public void Context()
        {
            var country = new CountryBuilder().Build();
            UnitOfWork.Save(country);

            _cinema = new CinemaBuilder().Build();
            UnitOfWork.Save(_cinema);

            _film = new FilmBuilder().Build();
            UnitOfWork.Save(_film);

            _location = new LocationBuilder()
                .WithCountry(country)
                .WithLocationFilms(new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new[]
                    {
                        new LocationFilmCinemaArgs { Cinema = _cinema }
                    }
                })
                .Build();
            UnitOfWork.Save(_location);

            UnitOfWork.Clear();

            _locationFilmCinemaDto = UnitOfWork.Session.QueryOver<LocationFilmDto>()
                .Where(x => x.LocationId == _location.Id)
                .List().Single()
                .Cinemas.SingleOrDefault();
        }

        [Test]
        public void location_film_cinema_dto_contains_correct_data()
        {
            _locationFilmCinemaDto.ShouldNotBeNull();
            _locationFilmCinemaDto.Id.ShouldBe(_location.Films.Single().Cinemas.Single().Id);
            _locationFilmCinemaDto.CinemaName.ShouldBe(_cinema.Name);
        }
    }
}