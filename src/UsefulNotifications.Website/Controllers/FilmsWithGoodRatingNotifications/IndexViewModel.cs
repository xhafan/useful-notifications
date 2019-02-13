using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class IndexViewModel
    {
        public string CountryCode { get; set; }
        public RatingSource RatingSource { get; set; }
        public string CsfdLocation { get; set; }
        public string ImdbPostCode { get; set; }
        public int? CsfdMinimalRating { get; set; }
        public decimal? ImdbMinimalRating { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> RatingSources { get; set; }

        public bool IsSelectedCountrySupportingCsfd { get; set; }    
        public bool IsCsfdRatingSelected { get; set; }    
    }
}