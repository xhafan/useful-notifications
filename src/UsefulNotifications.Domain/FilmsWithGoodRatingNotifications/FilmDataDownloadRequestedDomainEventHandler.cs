using Rebus.Bus.Advanced;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    // todo: test me
    public class FilmDataDownloadRequestedDomainEventHandler 
        : PublishDomainEventOverMessageBusDomainEventHandler<FilmDataDownloadRequestedDomainEvent>
    {
        public FilmDataDownloadRequestedDomainEventHandler(ISyncBus bus)
            : base(bus)
        {
        }
    }
}