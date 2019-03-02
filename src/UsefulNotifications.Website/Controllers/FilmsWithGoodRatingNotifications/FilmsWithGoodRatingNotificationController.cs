using System.Collections.Generic;
using System.Threading.Tasks;
using CoreDdd.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsefulNotifications.Commands;
using UsefulNotifications.Commands.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Queries.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.BusRequestSenders;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class FilmsWithGoodRatingNotificationController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IBusRequestSender _busRequestSender;

        public FilmsWithGoodRatingNotificationController(
            IQueryExecutor queryExecutor,
            IBusRequestSender busRequestSender
            )
        {
            _queryExecutor = queryExecutor;
            _busRequestSender = busRequestSender;
        }

        public IActionResult Index(SearchForFilmsArgs searchForFilmsArgs) // todo: test me
        {
            var viewModel = new IndexViewModel
            {
                CountryCode = searchForFilmsArgs.CountryCode ?? "CZ",
                RatingSource = searchForFilmsArgs.RatingSource,
                CsfdLocation = searchForFilmsArgs.CsfdLocation,
                ImdbPostCode = searchForFilmsArgs.ImdbPostCode,
                CsfdMinimalRating = searchForFilmsArgs.CsfdMinimalRating ?? 80m,
                ImdbMinimalRating = searchForFilmsArgs.ImdbMinimalRating ?? 8.0m,

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
        public async Task<IActionResult> TriggerLocationFilmDataDownload(SearchForFilmsArgs searchForFilmsArgs) // todo: test me
        {
            var reply = await _busRequestSender.SendRequest<CommandExecutedReply>(new DownloadLocationFilmDataCommand
            {
                CountryCode = searchForFilmsArgs.CountryCode,
                LocationNameOrPostCode = _GetLocationNameOrPostCodeFromSearchForFilmArgs(searchForFilmsArgs),
                RatingSource = searchForFilmsArgs.RatingSource
            });

            return RedirectToAction("WaitForLocationFilmDataDownload", searchForFilmsArgs);
        }

        public IActionResult WaitForLocationFilmDataDownload(SearchForFilmsArgs searchForFilmsArgs) // todo: test me
        {
            return View(searchForFilmsArgs);
        }

        public async Task<IActionResult> SearchForFilms(SearchForFilmsArgs searchForFilmsArgs)
        {
            var locationFilmDtos = await _queryExecutor.ExecuteAsync<GetFilmsQuery, LocationFilmDto>(new GetFilmsQuery
            {
                CountryCode = searchForFilmsArgs.CountryCode,
                RatingSource = searchForFilmsArgs.RatingSource,
                LocationNameOrPostCode = _GetLocationNameOrPostCodeFromSearchForFilmArgs(searchForFilmsArgs),
                MinimalRating = searchForFilmsArgs.RatingSource == RatingSource.Csfd 
                    ? searchForFilmsArgs.CsfdMinimalRating
                    : searchForFilmsArgs.ImdbMinimalRating
            });

            var viewModel = new SearchForFilmsViewModel
            {
                SearchForFilmsArgs = searchForFilmsArgs,
                Films = locationFilmDtos
            };
            return View(viewModel);
        }

        private string _GetLocationNameOrPostCodeFromSearchForFilmArgs(SearchForFilmsArgs searchForFilmsArgs)
        {
            return new HashSet<string> {"CZ", "SK"}.Contains(searchForFilmsArgs.CountryCode)
                ? searchForFilmsArgs.CsfdLocation
                : searchForFilmsArgs.ImdbPostCode;
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