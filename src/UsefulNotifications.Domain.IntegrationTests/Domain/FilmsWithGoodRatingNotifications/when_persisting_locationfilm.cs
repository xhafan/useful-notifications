using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Domain.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_location_film : BaseIntegrationTest
    {
        private Location _newLocation;
        private LocationFilm _persistedLocationFilm;
        private Country _country;
        private Film _film;

        [SetUp]
        public void Context()
        {
            _film = new FilmBuilder().Build();
            UnitOfWork.Save(_film);

            _country = new CountryBuilder().Build();
            UnitOfWork.Save(_country);

            _newLocation = new LocationBuilder()
                .WithCountry(_country)
                .WithLocationFilms(new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new LocationFilmCinemaArgs[0]
                })
                .Build();

            UnitOfWork.Save(_newLocation);
            UnitOfWork.Clear();

            _persistedLocationFilm = UnitOfWork.Get<Location>(_newLocation.Id).Films.SingleOrDefault();
        }

        [Test]
        public void persisted_location_film_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocationFilm.ShouldNotBeNull();
            _persistedLocationFilm.ShouldBe(_newLocation.Films.Single());
            _persistedLocationFilm.Film.ShouldBe(_film);
        }
    }
}