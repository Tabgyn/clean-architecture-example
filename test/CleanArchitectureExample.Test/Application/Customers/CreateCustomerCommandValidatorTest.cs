using CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanArchitectureExample.UnitTest.Application.Customers;

public class CreateCustomerCommandValidatorTest
{
    private readonly CreateCustomerCommandValidator _validator;

    public CreateCustomerCommandValidatorTest() => _validator = new CreateCustomerCommandValidator();

    [Fact]
    public void Validate_ShouldReturnError_WhenFirstNameIsEmpty()
    {
        //Arrange
        var customerCommandMock = new CreateCustomerCommand(
            "",
            "Borges",
            "tiago@email.com",
            new DateOnly(1990, 12, 31));

        //Act
        TestValidationResult<CreateCustomerCommand> result = _validator
            .TestValidate(customerCommandMock);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        result.ShouldNotHaveValidationErrorFor(result => result.Email);
        result.ShouldNotHaveValidationErrorFor(result => result.DateOfBirth);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenLastIsEmpty()
    {
        //Arrange
        var customerCommandMock = new CreateCustomerCommand(
            "Tiago",
            "",
            "tiago@email.com",
            new DateOnly(1990, 12, 31));

        //Act
        TestValidationResult<CreateCustomerCommand> result = _validator
            .TestValidate(customerCommandMock);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        result.ShouldNotHaveValidationErrorFor(result => result.Email);
        result.ShouldNotHaveValidationErrorFor(result => result.DateOfBirth);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmailIsEmpty()
    {
        //Arrange
        var customerCommandMock = new CreateCustomerCommand(
            "Tiago",
            "Borges",
            "",
            new DateOnly(1990, 12, 31));

        //Act
        TestValidationResult<CreateCustomerCommand> result = _validator
            .TestValidate(customerCommandMock);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        result.ShouldNotHaveValidationErrorFor(result => result.LastName);
        result.ShouldNotHaveValidationErrorFor(result => result.DateOfBirth);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmailIsInvalid()
    {
        //Arrange
        var customerCommandMock = new CreateCustomerCommand(
            "Tiago",
            "Borges",
            "email.com",
            new DateOnly(1990, 12, 31));

        //Act
        TestValidationResult<CreateCustomerCommand> result = _validator
            .TestValidate(customerCommandMock);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        result.ShouldNotHaveValidationErrorFor(result => result.LastName);
        result.ShouldNotHaveValidationErrorFor(result => result.DateOfBirth);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenDateOfBirthIsEmpty()
    {
        //Arrange
        var customerCommandMock = new CreateCustomerCommand(
            "Tiago",
            "Borges",
            "tiago@email.com",
            new DateOnly());

        //Act
        TestValidationResult<CreateCustomerCommand> result = _validator
            .TestValidate(customerCommandMock);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        result.ShouldNotHaveValidationErrorFor(result => result.LastName);
        result.ShouldNotHaveValidationErrorFor(result => result.Email);
    }
}
