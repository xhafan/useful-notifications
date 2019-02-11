using CoreDdd.Domain;

namespace UsefulNotifications.Domain.FilmsWithGoodRatingNotifications
{
    public class Country : Entity, IAggregateRoot
    {
        protected Country() {}

        public Country(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public virtual string Name { get; protected set; }
        public virtual string Code { get; protected set; }
    }
}