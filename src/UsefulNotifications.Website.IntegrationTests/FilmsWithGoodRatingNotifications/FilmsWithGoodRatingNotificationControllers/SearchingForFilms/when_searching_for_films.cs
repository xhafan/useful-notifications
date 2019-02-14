using System.Linq;
using System.Threading.Tasks;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.FilmsWithGoodRatingNotificationControllers.SearchingForFilms
{
    [TestFixture(TypeArgs = new[] { typeof (SearchingForFilmsWithImdbRatingSourceSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof (SearchingForFilmsWithCsfdRatingSourceSpecification) })]
    public class when_searching_for_films<TSearchingForFilmsSpecification>
        where TSearchingForFilmsSpecification : ISearchingForFilmsSpecification, new()
    {
        private NhibernateUnitOfWork _unitOfWork;
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        private IActionResult _actionResult;

        private Country _country;
        private Cinema _cinema;
        private Film _film;
        private Location _location;
        private SearchFilmsArgs _searchFilmsArgs;

        [SetUp]
        public async Task Context()
        {
            var specification = new TSearchingForFilmsSpecification();

            _serviceProvider = new ServiceProviderHelper().BuildServiceProvider();

            _serviceScope = _serviceProvider.CreateScope();

            _unitOfWork = _serviceProvider.GetService<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            _BuildAndSaveCountries();
            _BuildAndSaveCinamas();
            _BuildAndSaveFilms();
            _BuildAndSaveLocations();

            _unitOfWork.Clear();

            var controller = new FilmsWithGoodRatingNotificationControllerBuilder(_serviceProvider).Build();

            _searchFilmsArgs = specification.GetSearchFilmsArgs();

            _actionResult = await controller.SearchForFilms(_searchFilmsArgs);
        }

        [Test]
        public void action_result_is_view_result_with_correctly_populated_viewmodel()
        {
            _actionResult.ShouldBeOfType<ViewResult>();
            var viewResult = (ViewResult)_actionResult;

            viewResult.Model.ShouldBeOfType<SearchForFilmsViewModel>();
            var viewModel = (SearchForFilmsViewModel)viewResult.Model;

            viewModel.SearchFilmsArgs.ShouldBe(_searchFilmsArgs);
            viewModel.Films.Count().ShouldBe(1);
            var locationFilmDto = viewModel.Films.Single();
            locationFilmDto.Id.ShouldBe(_location.Films.Single().Id);
        }

        private void _BuildAndSaveLocations()
        {
            _location = new LocationBuilder()
                .WithCountry(_country)
                .WithNameOrPostCode("location name one")
                .WithLocationFilms(
                    new LocationFilmArgs
                    {
                        Film = _film,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinema}
                        }
                    }                    
                )
                .Build();

            _unitOfWork.Save(_location);
        }

        private void _BuildAndSaveFilms()
        {
            _film = new FilmBuilder()
                .WithFilmName("film name one")
                .WithFilmRatings(
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Imdb,
                        Rating = 8.2m,
                        Url = "film rating imdb url"
                    },
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Csfd,
                        Rating = 82m,
                        Url = "film rating csfd url"
                    }
                )
                .Build();
            _unitOfWork.Save(_film);
        }

        private void _BuildAndSaveCinamas()
        {
            _cinema = new CinemaBuilder().Build();
            _unitOfWork.Save(_cinema);
        }

        private void _BuildAndSaveCountries()
        {
            _country = new CountryBuilder()
                .WithCountryName("country one")
                .WithCountryCode("ONE")
                .Build();

            _unitOfWork.Save(_country);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }
    }
}