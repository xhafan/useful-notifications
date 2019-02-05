using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                    new SelectListItem {Value = "IMDB", Text = "IMDB"}
                }
            };
            viewModel.IsCountrySupportingCsfdSelected = viewModel.CountryCode == "CZ" || viewModel.CountryCode == "SK";
            viewModel.IsCsfdRatingSelected = viewModel.RatingSource == "CSFD";

            return View(viewModel);
        }

        public IActionResult SearchForFilms(SearchFilmsArgs searchFilmsArgs) // todo: test me
        {
            return View(searchFilmsArgs);
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