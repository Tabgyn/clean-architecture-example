using CleanArchitectureExample.Application.Common.Messaging;
using CleanArchitectureExample.Domain.Common.Errors;
using CleanArchitectureExample.Domain.Common.Interfaces.Persistence;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Common.Settings;
using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;
using Microsoft.Extensions.Options;

namespace CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;

internal sealed class CreateCustomerCommandHandler
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AnyOptions _anyOptions;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IOptions<AnyOptions> anyOptions)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _anyOptions = anyOptions.Value;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!await _customerRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(Errors.Customer.EmailAlreadyInUse);
        }

        var customer = Customer.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}
