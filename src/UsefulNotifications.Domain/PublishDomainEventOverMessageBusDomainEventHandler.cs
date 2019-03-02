using CoreDdd.Domain.Events;
using Rebus.Bus.Advanced;

namespace UsefulNotifications.Domain
{
    // todo: test me 
    public abstract class PublishDomainEventOverMessageBusDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly ISyncBus _bus;

        protected PublishDomainEventOverMessageBusDomainEventHandler(ISyncBus bus)
        {
            _bus = bus;
        }

        public void Handle(TDomainEvent domainEvent)
        {
            _bus.Publish(domainEvent);
        }
    }
}