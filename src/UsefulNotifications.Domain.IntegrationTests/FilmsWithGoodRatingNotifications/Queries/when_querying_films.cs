using System.Collections.Generic;
using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Queries.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.TestsShared.Builders.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain.IntegrationTests.FilmsWithGoodRatingNotifications.Queries
{
    [TestFixture]
    public class when_querying_films : BaseIntegrationTest
    {
        private IEnumerable<LocationFilmDto> _locationFilmDtos;
        private Country _countryOne;
        private Country _countryTwo;
        private Cinema _cinemaOne;
        private Cinema _cinemaTwo;
        private Cinema _cinemaThree;
        private Film _filmOne;
        private Film _filmTwo;
        private Location _locationOneCoutryOne;
        private Location _locationTwoCoutryOne;
        private Location _locationThreeCoutryTwo;

        [SetUp]
        public void Context()
        {
            _BuildAndSaveCountries();
            _BuildAndSaveCinamas();
            _BuildAndSaveFilms();
            _BuildAndSaveLocations();

            UnitOfWork.Clear();

            var queryHandler = new GetFilmsQueryHandler(UnitOfWork);

            _locationFilmDtos = queryHandler.Execute<LocationFilmDto>(new GetFilmsQuery
            {
                CountryId = _countryOne.Id,
                LocationNameOrPostCode = "location name one",
                RatingSource = RatingSource.Imdb,
                MinimalRating = 8.2m
            });
        }

        [Test]
        public void only_correct_location_film_dtos_are_queried()
        {
            _locationFilmDtos.Count().ShouldBe(1);
            var locationFilmDto = _locationFilmDtos.Single();
            locationFilmDto.Id.ShouldBe(_locationOneCoutryOne.Films.Single(x => x.Film.Name == "film name one").Id);
        }

        private void _BuildAndSaveLocations()
        {
            _locationOneCoutryOne = new LocationBuilder()
                .WithCountry(_countryOne)
                .WithNameOrPostCode("location name one")
                .WithLocationFilms(
                    new LocationFilmArgs
                    {
                        Film = _filmOne,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaOne},
                            new LocationFilmCinemaArgs {Cinema = _cinemaTwo}
                        }
                    },
                    new LocationFilmArgs
                    {
                        Film = _filmTwo,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaTwo}
                        }
                    }
                )
                .Build();
            _locationTwoCoutryOne = new LocationBuilder()
                .WithCountry(_countryOne)
                .WithNameOrPostCode("location name two")
                .WithLocationFilms(
                    new LocationFilmArgs
                    {
                        Film = _filmOne,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaOne},
                            new LocationFilmCinemaArgs {Cinema = _cinemaTwo}
                        }
                    },
                    new LocationFilmArgs
                    {
                        Film = _filmTwo,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaTwo}
                        }
                    }
                )
                .Build();
            _locationThreeCoutryTwo = new LocationBuilder()
                .WithCountry(_countryTwo)
                .WithNameOrPostCode("location name three")
                .WithLocationFilms(
                    new LocationFilmArgs
                    {
                        Film = _filmOne,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaThree}
                        }
                    },
                    new LocationFilmArgs
                    {
                        Film = _filmTwo,
                        Cinemas = new[]
                        {
                            new LocationFilmCinemaArgs {Cinema = _cinemaThree}
                        }
                    }
                )
                .Build();

            UnitOfWork.Save(_locationOneCoutryOne);
            UnitOfWork.Save(_locationTwoCoutryOne);
            UnitOfWork.Save(_locationThreeCoutryTwo);
        }

        private void _BuildAndSaveFilms()
        {
            _filmOne = new FilmBuilder()
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
            _filmTwo = new FilmBuilder()
                .WithFilmName("film name two")
                .WithFilmRatings(
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Imdb,
                        Rating = 8.1m,
                        Url = "film rating imdb url"
                    },
                    new FilmRatingArgs
                    {
                        Source = RatingSource.Csfd,
                        Rating = 81m,
                        Url = "film rating csfd url"
                    }
                )
                .Build();
            UnitOfWork.Save(_filmOne);
            UnitOfWork.Save(_filmTwo);
        }

        private void _BuildAndSaveCinamas()
        {
            _cinemaOne = new CinemaBuilder().Build();
            _cinemaTwo = new CinemaBuilder().Build();
            _cinemaThree = new CinemaBuilder().Build();
            UnitOfWork.Save(_cinemaOne);
            UnitOfWork.Save(_cinemaTwo);
            UnitOfWork.Save(_cinemaThree);
        }

        private void _BuildAndSaveCountries()
        {
            _countryOne = new CountryBuilder()
                .WithCountryName("country one")
                .WithCountryCode("ONE")
                .Build();

            _countryTwo = new CountryBuilder()
                .WithCountryName("country two")
                .WithCountryCode("TWO")
                .Build();

            UnitOfWork.Save(_countryOne);
            UnitOfWork.Save(_countryTwo);
        }    
    }
}