using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_country
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Country _newCountry;
        private Country _persistedCountry;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _newCountry = new Country("country code", "country name");

            _unitOfWork.Save(_newCountry);
            _unitOfWork.Clear();

            _persistedCountry = _unitOfWork.Get<Country>(_newCountry.Id);
        }

        [Test]
        public void persisted_country_can_be_retrieved_and_has_the_same_data()
        {
            _persistedCountry.ShouldNotBeNull();
            _persistedCountry.ShouldBe(_newCountry);
            _persistedCountry.Code.ShouldBe("country code");
            _persistedCountry.Name.ShouldBe("country name");
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}