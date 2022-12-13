using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Entity(IEntity ent)
        {
            Id = ent.Id;
        }


        public Guid Id { get; set; }


        public bool Equals(Entity? other) => other?.Id.Equals(Id) ?? false;

        public override bool Equals(object? obj) => obj is Entity entity && Equals(entity);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
