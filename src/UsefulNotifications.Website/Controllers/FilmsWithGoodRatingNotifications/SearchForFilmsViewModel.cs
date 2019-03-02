using System.Collections.Generic;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class SearchForFilmsViewModel
    {
        public SearchForFilmsArgs SearchForFilmsArgs { get; set; }
        public IEnumerable<LocationFilmDto> Films { get; set; }
    }
}