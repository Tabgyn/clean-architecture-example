using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.ValueObjects;

namespace CleanArchitectureExample.Persistence.Specifications;

internal class CustomerPagedSpecification : Specification<Customer, CustomerId>
{
    public CustomerPagedSpecification(int take, int skip)
        : base(null)
    {
        ApplyPaging(take, skip);
    }
}
