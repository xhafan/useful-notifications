using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class LocationFilmDataDownloadRequestMappingOverrides : IAutoMappingOverride<LocationFilmDataDownloadRequest>
    {
        public void Override(AutoMapping<LocationFilmDataDownloadRequest> mapping)
        {
            mapping.References(x => x.Location).Nullable();
        }
    }
}