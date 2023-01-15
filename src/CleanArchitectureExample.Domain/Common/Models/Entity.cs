using System.Diagnostics.CodeAnalysis;

namespace CleanArchitectureExample.Domain.Common.Models;

[ExcludeFromCodeCoverage]
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    protected Entity(TId id) => Id = id;

    public TId Id { get; protected set; }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second) =>
        first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) =>
        !(first == second);

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is not null
            && obj.GetType() == GetType()
            && obj is Entity<TId> entity
            && Id.Equals(entity.Id);
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}
