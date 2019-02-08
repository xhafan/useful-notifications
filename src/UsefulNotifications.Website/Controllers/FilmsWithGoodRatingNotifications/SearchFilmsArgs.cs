using System.Collections.Generic;
using System.Globalization;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class SearchFilmsArgs
    {
        public string CountryCode { get; set; }
        public string RatingSource { get; set; }
        public string CsfdLocation { get; set; }
        public string ImdbPostCode { get; set; }
        public int? CsfdMinimalRating { get; set; }
        public decimal? ImdbMinimalRating { get; set; }

        public IDictionary<string, string> ToRouteData() // todo: test me
        {
            return new Dictionary<string, string>
            {
                { nameof(CountryCode), CountryCode },
                { nameof(RatingSource), RatingSource },
                { nameof(CsfdLocation), CsfdLocation },
                { nameof(ImdbPostCode), ImdbPostCode },
                { nameof(CsfdMinimalRating), CsfdMinimalRating.ToString() },
                { nameof(ImdbMinimalRating), ImdbMinimalRating?.ToString(CultureInfo.InvariantCulture) } // todo: check different culture settings
            };
        }
    }
}