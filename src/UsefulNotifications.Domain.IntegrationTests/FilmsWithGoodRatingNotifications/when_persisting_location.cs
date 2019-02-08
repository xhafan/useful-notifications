using System;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_location
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Location _newLocation;
        private Location _persistedLocation;
        private Country _country;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _country = new Country("country code", "country name");
            _unitOfWork.Save(_country);

            _newLocation = new Location(_country, "name or post code", new LocationFilmArgs[0]);

            _unitOfWork.Save(_newLocation);
            _unitOfWork.Clear();

            _persistedLocation = _unitOfWork.Get<Location>(_newLocation.Id);
        }

        [Test]
        public void persisted_location_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocation.ShouldNotBeNull();
            _persistedLocation.ShouldBe(_newLocation);
            _persistedLocation.Country.ShouldBe(_country);
            _persistedLocation.NameOrPostCode.ShouldBe("name or post code");
            _persistedLocation.LastUpdated.ShouldBeInRange(DateTime.Now.AddSeconds(-10), DateTime.Now.AddSeconds(+10));
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}