using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Common.ValueObjects;
using CleanArchitectureExample.Domain.Customers.ValueObjects;

namespace CleanArchitectureExample.Domain.Customers;

public sealed class Customer : AggregateRoot<CustomerId>
{
    private Customer()
        : base(CustomerId.Empty)
    { }

    private Customer(
        CustomerId id,
        string firstName,
        string lastName,
        Email email,
        DateOnly dateOfBirth)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public Email Email { get; } = null!;
    public DateOnly DateOfBirth { get; }

    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        DateOnly dateOfBirth)
    {
        return new(
            CustomerId.CreateUnique(),
            firstName,
            lastName,
            Email.Create(email),
            dateOfBirth);
    }
}
