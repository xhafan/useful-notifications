using System.Collections.Generic;
using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Film : Entity, IAggregateRoot
    {
        private readonly ICollection<FilmRating> _filmRatings = new HashSet<FilmRating>();

        protected Film() {}

        public Film(string name, string mainUrl, IEnumerable<FilmRatingArgs> filmRatingArgses)
        {
            Name = name;
            MainUrl = mainUrl;
            foreach (var filmRatingArgs in filmRatingArgses)
            {
                _filmRatings.Add(new FilmRating(this, filmRatingArgs));
            }
        }

        public virtual string Name { get; protected set; }
        public virtual string MainUrl { get; protected set; }
        public virtual IEnumerable<FilmRating> FilmRatings => _filmRatings;
    }
}