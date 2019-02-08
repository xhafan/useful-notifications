using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Country : Entity, IAggregateRoot
    {
        protected Country() {}

        public Country(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public virtual string Code { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}