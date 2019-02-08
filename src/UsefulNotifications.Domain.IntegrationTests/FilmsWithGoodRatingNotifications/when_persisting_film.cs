using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_film
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Film _newFilm;
        private Film _persistedFilm;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _newFilm = new Film("film name", "film url", new FilmRatingArgs[0]);

            _unitOfWork.Save(_newFilm);
            _unitOfWork.Clear();

            _persistedFilm = _unitOfWork.Get<Film>(_newFilm.Id);
        }

        [Test]
        public void persisted_film_can_be_retrieved_and_has_the_same_data()
        {
            _persistedFilm.ShouldNotBeNull();
            _persistedFilm.ShouldBe(_newFilm);
            _persistedFilm.Name.ShouldBe("film name");
            _persistedFilm.MainUrl.ShouldBe("film url");
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}