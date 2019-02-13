using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.IntegrationTestsShared;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_location_film_cinema : BaseIntegrationTest
    {
        private Location _newLocation;
        private LocationFilmCinema _persistedLocationFilmCinema;
        private Country _country;
        private Film _film;
        private Cinema _cinema;

        [SetUp]
        public void Context()
        {
            _cinema = new CinemaBuilder().Build();
            UnitOfWork.Save(_cinema);

            _film = new FilmBuilder().Build();
            UnitOfWork.Save(_film);

            _country = new CountryBuilder().Build();
            UnitOfWork.Save(_country);

            _newLocation = new LocationBuilder()
                .WithCountry(_country)
                .WithLocationFilms(new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new[]
                    {
                        new LocationFilmCinemaArgs { Cinema = _cinema }
                    }
                })
                .Build();

            UnitOfWork.Save(_newLocation);
            UnitOfWork.Clear();

            _persistedLocationFilmCinema = UnitOfWork.Get<Location>(_newLocation.Id).Films.Single().Cinemas.SingleOrDefault();
        }

        [Test]
        public void persisted_location_film_ciname_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocationFilmCinema.ShouldNotBeNull();
            _persistedLocationFilmCinema.ShouldBe(_newLocation.Films.Single().Cinemas.Single());
            _persistedLocationFilmCinema.Cinema.ShouldBe(_cinema);
        }
    }
}