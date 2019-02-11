using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.Domain.FilmsWithGoodRatingNotifications
{
    [TestFixture]
    public class when_persisting_film_rating : BaseIntegrationTest
    {
        private Film _newFilm;
        private FilmRating _persistedFilmRating;

        [SetUp]
        public void Context()
        {
            _newFilm = new FilmBuilder()
                .WithFilmRatings(new FilmRatingArgs
                {
                    Source = RatingSource.Imdb,
                    Rating = "8.2",
                    Url = "film rating url"
                })
                .Build();

            UnitOfWork.Save(_newFilm);
            UnitOfWork.Clear();

            _persistedFilmRating = UnitOfWork.Get<Film>(_newFilm.Id).FilmRatings.SingleOrDefault();
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
    }
}