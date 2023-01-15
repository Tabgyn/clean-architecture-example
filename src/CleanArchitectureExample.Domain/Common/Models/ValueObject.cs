using System.Diagnostics.CodeAnalysis;

namespace CleanArchitectureExample.Domain.Common.Models;

[ExcludeFromCodeCoverage]
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public static bool operator ==(ValueObject? first, ValueObject? second) =>
    first is not null && second is not null && first.Equals(second);

    public static bool operator !=(ValueObject? first, ValueObject? second) =>
    !(first == second);

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is not null
            && obj.GetType() == GetType()
            && obj is ValueObject valueObject
            && GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
