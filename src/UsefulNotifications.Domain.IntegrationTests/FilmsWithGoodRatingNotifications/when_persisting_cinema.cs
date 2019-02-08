using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_cinema
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Cinema _newCinema;
        private Cinema _persistedCinema;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _newCinema = new Cinema("cinema name");

            _unitOfWork.Save(_newCinema);
            _unitOfWork.Clear();

            _persistedCinema = _unitOfWork.Get<Cinema>(_newCinema.Id);
        }

        [Test]
        public void persisted_cinema_can_be_retrieved_and_has_the_same_data()
        {
            _persistedCinema.ShouldNotBeNull();
            _persistedCinema.ShouldBe(_newCinema);
            _persistedCinema.Name.ShouldBe("cinema name");
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}