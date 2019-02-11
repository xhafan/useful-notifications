using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Domain.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_country : BaseIntegrationTest
    {
        private Country _newCountry;
        private Country _persistedCountry;

        [SetUp]
        public void Context()
        {
            _newCountry = new CountryBuilder().Build();

            UnitOfWork.Save(_newCountry);
            UnitOfWork.Clear();

            _persistedCountry = UnitOfWork.Get<Country>(_newCountry.Id);
        }

        [Test]
        public void persisted_country_can_be_retrieved_and_has_the_same_data()
        {
            _persistedCountry.ShouldNotBeNull();
            _persistedCountry.ShouldBe(_newCountry);
            _persistedCountry.Name.ShouldBe(CountryBuilder.CountryName);
            _persistedCountry.Code.ShouldBe(CountryBuilder.CountryCode);
        }
    }
}