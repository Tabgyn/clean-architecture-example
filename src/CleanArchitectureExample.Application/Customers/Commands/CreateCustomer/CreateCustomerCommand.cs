using CleanArchitectureExample.Application.Common.Messaging;

namespace CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth) : ICommand<Guid>;
