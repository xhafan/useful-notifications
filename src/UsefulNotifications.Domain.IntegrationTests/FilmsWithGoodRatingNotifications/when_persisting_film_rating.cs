using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_film_rating
    {
        private NhibernateUnitOfWork _unitOfWork;
        private Film _newFilm;
        private FilmRating _persistedFilmRating;

        [SetUp]
        public void Context()
        {
            _unitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            _unitOfWork.BeginTransaction();

            _newFilm = new Film("film name", "film url", new[]
            {
                new FilmRatingArgs
                {
                    Source = RatingSource.Imdb,
                    Rating = "8.2",
                    Url = "film rating url"
                }
            });

            _unitOfWork.Save(_newFilm);
            _unitOfWork.Clear();

            _persistedFilmRating = _unitOfWork.Get<Film>(_newFilm.Id).FilmRatings.SingleOrDefault();
        }

        [Test]
        public void persisted_film_can_be_retrieved_and_has_the_same_data()
        {
            _persistedFilmRating.ShouldNotBeNull();
            _persistedFilmRating.ShouldBe(_newFilm.FilmRatings.Single());
            _persistedFilmRating.Source.ShouldBe(RatingSource.Imdb);
            _persistedFilmRating.Rating.ShouldBe("8.2");
            _persistedFilmRating.Url.ShouldBe("film rating url");
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
        }
    }
}