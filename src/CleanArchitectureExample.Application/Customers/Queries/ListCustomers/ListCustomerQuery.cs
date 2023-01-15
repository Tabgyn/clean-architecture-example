using CleanArchitectureExample.Application.Common.Messaging;
using CleanArchitectureExample.Domain.Customers;

namespace CleanArchitectureExample.Application.Customers.Queries.ListCustomers;

public sealed record ListCustomerQuery(int Take, int Skip)
    : IQuery<IList<Customer>>;
