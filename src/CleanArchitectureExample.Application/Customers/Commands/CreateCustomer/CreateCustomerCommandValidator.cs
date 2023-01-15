using FluentValidation;

namespace CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;

internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("'{PropertyValue}' is not a valid email address.");

        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}
