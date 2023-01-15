using System.Diagnostics.CodeAnalysis;
using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Customers.ValueObjects;

[ExcludeFromCodeCoverage]
public sealed class CustomerId : ValueObject
{
    private CustomerId(Guid value) => Value = value;

    public Guid Value { get; }

    public static readonly CustomerId Empty = new(Guid.Empty);

    public static CustomerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator CustomerId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}
