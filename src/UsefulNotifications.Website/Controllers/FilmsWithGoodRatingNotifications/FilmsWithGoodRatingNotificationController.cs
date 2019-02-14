using System.Threading.Tasks;
using CoreDdd.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Queries.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class FilmsWithGoodRatingNotificationController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;

        public FilmsWithGoodRatingNotificationController(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public IActionResult Index(SearchFilmsArgs searchFilmsArgs) // todo: test me
        {
            var viewModel = new IndexViewModel
            {
                CountryCode = searchFilmsArgs.CountryCode ?? "CZ",
                RatingSource = searchFilmsArgs.RatingSource,
                CsfdLocation = searchFilmsArgs.CsfdLocation,
                ImdbPostCode = searchFilmsArgs.ImdbPostCode,
                CsfdMinimalRating = searchFilmsArgs.CsfdMinimalRating ?? 80m,
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
            viewModel.IsCsfdRatingSelected = viewModel.RatingSource == RatingSource.Csfd;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchForFilms(SearchFilmsArgs searchFilmsArgs)
        {
            var locationFilmDtos = await _queryExecutor.ExecuteAsync<GetFilmsQuery, LocationFilmDto>(new GetFilmsQuery
            {
                CountryCode = searchFilmsArgs.CountryCode,
                RatingSource = searchFilmsArgs.RatingSource,
                LocationNameOrPostCode = searchFilmsArgs.RatingSource == RatingSource.Csfd
                    ? searchFilmsArgs.CsfdLocation
                    : searchFilmsArgs.ImdbPostCode,
                MinimalRating = searchFilmsArgs.RatingSource == RatingSource.Csfd 
                    ? searchFilmsArgs.CsfdMinimalRating
                    : searchFilmsArgs.ImdbMinimalRating
            });

            var viewModel = new SearchForFilmsViewModel
            {
                SearchFilmsArgs = searchFilmsArgs,
                Films = locationFilmDtos
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