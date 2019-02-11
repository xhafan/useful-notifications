using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class FilmsWithGoodRatingNotificationController : Controller
    {
        public IActionResult Index(SearchFilmsArgs searchFilmsArgs) // todo: test me
        {
            var viewModel = new IndexViewModel
            {
                CountryCode = searchFilmsArgs.CountryCode ?? "CZ",
                RatingSource = searchFilmsArgs.RatingSource ?? "CSFD",
                CsfdLocation = searchFilmsArgs.CsfdLocation,
                ImdbPostCode = searchFilmsArgs.ImdbPostCode,
                CsfdMinimalRating = searchFilmsArgs.CsfdMinimalRating ?? 80,
                ImdbMinimalRating = searchFilmsArgs.ImdbMinimalRating ?? 8.0m,

                Countries = new[]
                {
                    new SelectListItem {Value = "CZ", Text = "Česká republika"},
                    new SelectListItem {Value = "SK", Text = "Slovensko"},
                    new SelectListItem {Value = "UK", Text = "Velká Británie"},
                },
                RatingSources = new[]
                {
                    new SelectListItem {Value = "CSFD", Text = "ČSFD"},
                    new SelectListItem {Value = "IMDB", Text = "IMDb"}
                }
            };
            viewModel.IsSelectedCountrySupportingCsfd = viewModel.CountryCode == "CZ" || viewModel.CountryCode == "SK";
            viewModel.IsCsfdRatingSelected = viewModel.RatingSource == "CSFD";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchForFilms(SearchFilmsArgs searchFilmsArgs) // todo: test me
        {
            var viewModel = new SearchForFilmsViewModel
            {
                SearchFilmsArgs = searchFilmsArgs,
                Films = new[]
                {
                    new LocationFilmDto
                    {
                        FilmName = "Ženy v běhu",
                        FilmMainUrl = "https://www.csfd.cz/film/657646-zeny-v-behu/prehled/",
                        Ratings = new[]
                        {
                            new FilmRatingDto
                            {
                                RatingSource = "ČSFD",
                                FilmUrl = "https://www.csfd.cz/film/657646-zeny-v-behu/prehled/",
                                Rating = "82%",
                            },
                            new FilmRatingDto
                            {
                                RatingSource = "IMDB",
                                FilmUrl = "https://www.imdb.com/title/tt8938852/",
                                Rating = "7.8",
                            }
                        },
                        Cinemas = new[] {new LocationFilmCinemaDto {CinemaName = "Zlín Golden Apple Cinema"}}
                    }
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNotification() // todo: test me
        {
            return RedirectToAction("NotificationAdded");

        }

        public IActionResult NotificationAdded() // todo: test me
        {
            return View();
        }
    }
}