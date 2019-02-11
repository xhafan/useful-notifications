using System.Collections.Generic;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class SearchForFilmsViewModel
    {
        public SearchFilmsArgs SearchFilmsArgs { get; set; }
        public IEnumerable<LocationFilmDto> Films { get; set; }
    }
}