using System;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications.Domain
{
    [TestFixture]
    public class when_persisting_location : BaseIntegrationTest
    {
        private Location _newLocation;
        private Location _persistedLocation;
        private Country _country;

        [SetUp]
        public void Context()
        {
            _country = new CountryBuilder().Build();
            UnitOfWork.Save(_country);

            _newLocation = new LocationBuilder()
                .WithCountry(_country)
                .Build();

            UnitOfWork.Save(_newLocation);
            UnitOfWork.Clear();

            _persistedLocation = UnitOfWork.Get<Location>(_newLocation.Id);
        }

        [Test]
        public void persisted_location_can_be_retrieved_and_has_the_same_data()
        {
            _persistedLocation.ShouldNotBeNull();
            _persistedLocation.ShouldBe(_newLocation);
            _persistedLocation.Country.ShouldBe(_country);
            _persistedLocation.NameOrPostCode.ShouldBe(LocationBuilder.NameOrPostCode);
            _persistedLocation.LastUpdated.ShouldBeInRange(DateTime.Now.AddSeconds(-10), DateTime.Now.AddSeconds(+10));
        }
    }
}