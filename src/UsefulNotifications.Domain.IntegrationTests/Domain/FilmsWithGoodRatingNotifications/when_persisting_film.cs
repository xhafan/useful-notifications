using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Domain.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_film : BaseIntegrationTest
    {
        private Film _newFilm;
        private Film _persistedFilm;

        [SetUp]
        public void Context()
        {
            _newFilm = new FilmBuilder().Build();

            UnitOfWork.Save(_newFilm);
            UnitOfWork.Clear();

            _persistedFilm = UnitOfWork.Get<Film>(_newFilm.Id);
        }

        [Test]
        public void persisted_film_can_be_retrieved_and_has_the_same_data()
        {
            _persistedFilm.ShouldNotBeNull();
            _persistedFilm.ShouldBe(_newFilm);
            _persistedFilm.Name.ShouldBe(FilmBuilder.FilmName);
            _persistedFilm.MainUrl.ShouldBe(FilmBuilder.FilmUrl);
        }
    }
}