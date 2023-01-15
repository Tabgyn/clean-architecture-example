namespace CleanArchitectureExample.Api.Contracts.Customers;

public sealed record CustomerResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth)
{
    public string FullName { get; init; } = $"{FirstName} {LastName}";
};
