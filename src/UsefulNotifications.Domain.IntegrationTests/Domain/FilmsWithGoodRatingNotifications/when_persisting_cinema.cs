using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Domain.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_cinema : BaseIntegrationTest
    {
        private Cinema _newCinema;
        private Cinema _persistedCinema;

        [SetUp]
        public void Context()
        {
            _newCinema = new CinemaBuilder().Build();

            UnitOfWork.Save(_newCinema);
            UnitOfWork.Clear();

            _persistedCinema = UnitOfWork.Get<Cinema>(_newCinema.Id);
        }

        [Test]
        public void persisted_cinema_can_be_retrieved_and_has_the_same_data()
        {
            _persistedCinema.ShouldNotBeNull();
            _persistedCinema.ShouldBe(_newCinema);
            _persistedCinema.Name.ShouldBe(CinemaBuilder.CinemaName);
        }
    }
}