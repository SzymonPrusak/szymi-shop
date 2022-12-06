﻿namespace SzymiShop.WebApi.Business.Model
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }


        public Guid Id { get; }


        public bool Equals(Entity? other) => other?.Id.Equals(Id) ?? false;

        public override bool Equals(object? obj) => obj is Entity entity && Equals(entity);
        public override int GetHashCode() => Id.GetHashCode();
    }
}