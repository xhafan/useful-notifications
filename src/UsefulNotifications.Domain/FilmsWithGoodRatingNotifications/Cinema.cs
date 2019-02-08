using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Cinema : Entity, IAggregateRoot
    {
        protected Cinema() {}

        public Cinema(string name)
        {
            Name = name;
        }

        public virtual string Name { get; protected set; }
    }
}