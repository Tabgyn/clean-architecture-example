using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.ValueObjects;

namespace CleanArchitectureExample.Persistence.Specifications;

internal class CustomerByIdSpecification : Specification<Customer, CustomerId>
{
    public CustomerByIdSpecification(Guid customerId)
        : base(customer => customer.Id.Value == customerId)
    {
    }
}
