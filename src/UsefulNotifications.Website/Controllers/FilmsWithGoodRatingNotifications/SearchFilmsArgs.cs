using System.Collections.Generic;
using System.Globalization;
using UsefulNotifications.Shared.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications
{
    public class SearchFilmsArgs
    {
        public string CountryCode { get; set; }
        public RatingSource RatingSource { get; set; }

        public string CsfdLocation { get; set; }
        public decimal? CsfdMinimalRating { get; set; }

        public string ImdbPostCode { get; set; }
        public decimal? ImdbMinimalRating { get; set; }

        public IDictionary<string, string> ToRouteData() // todo: test me
        {
            return new Dictionary<string, string>
            {
                { nameof(CountryCode), CountryCode },
                { nameof(RatingSource), RatingSource.ToString() },
                { nameof(CsfdLocation), CsfdLocation },
                { nameof(ImdbPostCode), ImdbPostCode },
                { nameof(CsfdMinimalRating), CsfdMinimalRating.ToString() },
                { nameof(ImdbMinimalRating), ImdbMinimalRating?.ToString(CultureInfo.InvariantCulture) } // todo: check different culture settings
            };
        }
    }
}