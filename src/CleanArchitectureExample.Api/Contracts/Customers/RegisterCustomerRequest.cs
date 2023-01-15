namespace CleanArchitectureExample.Api.Contracts.Customers;

public sealed record RegisterCustomerRequest(
    string Email,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth);
