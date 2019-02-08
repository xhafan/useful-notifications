using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_location_film_cinema
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Location _newLocation;
        private LocationFilmCinema _persistedLocationFilmCinema;
        private Country _country;
        private Film _film;
        private Cinema _cinema;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _cinema = new Cinema("cinema name");
            _unitOfWork.Save(_cinema);

            _film = new Film("film name", "film main url", new FilmRatingArgs[0]);
            _unitOfWork.Save(_film);

            _country = new Country("country code", "country name");
            _unitOfWork.Save(_country);

            _newLocation = new Location(_country, "name or post code", new[]
            {
                new LocationFilmArgs
                {
                    Film = _film,
                    Cinemas = new[]
                    {
                        new LocationFilmCinemaArgs { Cinema = _cinema }
                    }
                }
            });

            _unitOfWork.Save(_newLocation);
            _unitOfWork.Clear();

            _persistedLocationFilmCinema = _unitOfWork.Get<Location>(_newLocation.Id).Films.Single().Cinemas.SingleOrDefault();
        }

        [Test]
        public void persisted_location_film_ciname_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocationFilmCinema.ShouldNotBeNull();
            _persistedLocationFilmCinema.ShouldBe(_newLocation.Films.Single().Cinemas.Single());
            _persistedLocationFilmCinema.Cinema.ShouldBe(_cinema);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}