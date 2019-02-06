using System.Collections.Generic;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class SearchForFilmsViewModel
    {
        public SearchFilmsArgs SearchFilmsArgs { get; set; }
        public IEnumerable<FilmViewModel> Films { get; set; }
    }
}