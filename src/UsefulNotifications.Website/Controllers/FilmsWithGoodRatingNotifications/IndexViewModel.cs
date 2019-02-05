using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class IndexViewModel
    {
        public string CountryCode { get; set; }
        public string RatingSource { get; set; }
        public string CsfdLocation { get; set; }
        public string ImdbPostCode { get; set; }
        public int? CsfdMinimalRating { get; set; }
        public decimal? ImdbMinimalRating { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> RatingSources { get; set; }

        public bool IsCountrySupportingCsfdSelected { get; set; }    
        public bool IsCsfdRatingSelected { get; set; }    
    }
}