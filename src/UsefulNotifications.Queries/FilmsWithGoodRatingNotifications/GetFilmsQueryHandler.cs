using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Queries.FilmsWithGoodRatingNotifications
{
    public class GetFilmsQueryHandler : BaseQueryOverHandler<GetFilmsQuery>
    {
        public GetFilmsQueryHandler(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override IQueryOver GetQueryOver<TResult>(GetFilmsQuery query)
        {
            FilmRatingDto filmRatingDto = null;
            return Session.QueryOver<LocationFilmDto>()
                    .Where(x => x.CountryId == query.CountryId
                                && x.LocationNameOrPostCode == query.LocationNameOrPostCode
                    )
                    .JoinAlias(x => x.Ratings, () => filmRatingDto)
                    .Where(() => filmRatingDto.RatingSource == query.RatingSource
                                 && filmRatingDto.Rating >= query.MinimalRating)
                ;
        }
    }
}