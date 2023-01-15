using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.ValueObjects;

namespace CleanArchitectureExample.Persistence.Specifications;

internal class CustomerByEmailSpecification : Specification<Customer, CustomerId>
{
    public CustomerByEmailSpecification(string email)
        : base(customer => customer.Email == email)
    {
    }
}
