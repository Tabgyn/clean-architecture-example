using CleanArchitectureExample.Application.Common.Messaging;
using CleanArchitectureExample.Domain.Common.Errors;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;

namespace CleanArchitectureExample.Application.Customers.Queries.GetCustomerById;

internal sealed class GetCustomerByIdQueryHandler
    : IQueryHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<Customer>> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken);

        return customer is null
            ? Result.Failure<Customer>(Errors.General.NotFound(request.CustomerId))
            : customer;
    }
}
