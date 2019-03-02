using CoreDdd.Commands;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Commands.FilmsWithGoodRatingNotifications
{
    public class DownloadLocationFilmDataCommand : ICommand
    {
        public string CountryCode { get; set; }
        public string LocationNameOrPostCode { get; set; }
        public RatingSource RatingSource { get; set; }
    }
}