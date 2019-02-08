using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_location_film
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Location _newLocation;
        private LocationFilm _persistedLocationFilm;
        private Country _country;
        private Film _film;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _film = new Film("film name", "film main url", new FilmRatingArgs[0]);
            _unitOfWork.Save(_film);

            _country = new Country("country code", "country name");
            _unitOfWork.Save(_country);

            _newLocation = new Location(_country, "name or post code", new[]
            {
                new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new LocationFilmCinemaArgs[0]
                }
            });

            _unitOfWork.Save(_newLocation);
            _unitOfWork.Clear();

            _persistedLocationFilm = _unitOfWork.Get<Location>(_newLocation.Id).Films.SingleOrDefault();
        }

        [Test]
        public void persisted_location_film_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocationFilm.ShouldNotBeNull();
            _persistedLocationFilm.ShouldBe(_newLocation.Films.Single());
            _persistedLocationFilm.Film.ShouldBe(_film);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}