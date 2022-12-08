using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        [SetsRequiredMembers]
        public Entity(Guid id)
        {
            Id = id;
        }

        [SetsRequiredMembers]
        public Entity(IEntity ent)
        {
            Id = ent.Id;
        }


        public required Guid Id { get; set; }


        public bool Equals(Entity? other) => other?.Id.Equals(Id) ?? false;

        public override bool Equals(object? obj) => obj is Entity entity && Equals(entity);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
