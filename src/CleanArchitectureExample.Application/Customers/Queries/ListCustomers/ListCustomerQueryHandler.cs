using CleanArchitectureExample.Application.Common.Messaging;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;

namespace CleanArchitectureExample.Application.Customers.Queries.ListCustomers;

internal sealed class ListCustomerQueryHandler
    : IQueryHandler<ListCustomerQuery, IList<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public ListCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<IList<Customer>>> Handle(
        ListCustomerQuery request,
        CancellationToken cancellationToken)
    {
        IList<Customer> customers = await _customerRepository.GetAllAsync(
            request.Take,
            request.Skip,
            cancellationToken);

        return (Result<IList<Customer>>)customers;
    }
}
