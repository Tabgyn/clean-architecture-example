using CleanArchitectureExample.Application.Common.Messaging;
using CleanArchitectureExample.Domain.Customers;

namespace CleanArchitectureExample.Application.Customers.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(Guid CustomerId) : IQuery<Customer>;
