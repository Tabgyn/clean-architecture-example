using CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;
using CleanArchitectureExample.Domain.Common.Errors;
using CleanArchitectureExample.Domain.Common.Interfaces.Persistence;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Common.Settings;
using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CleanArchitectureExample.UnitTest.Application.Customers;

public class CreateCustomerCommandHandlerTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AnyOptions _anyOptions;
    private readonly CreateCustomerCommandHandler _sut;

    public CreateCustomerCommandHandlerTest()
    {
        _customerRepositoryMock = new();
        _unitOfWorkMock = new();
        _anyOptions = new AnyOptions { SomeName = "Testing", SomeValue = 99 };
        var anyOptions = Options.Create(_anyOptions);
        _sut = new CreateCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            anyOptions);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenEmailIsNotUnique()
    {
        //Arrange
        CreateCustomerCommand request = BuildCreateCustomerCommand();

        _customerRepositoryMock.Setup(
            e => e.IsEmailUniqueAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        Result<Guid> result = await _sut.Handle(request, default);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.Customer.EmailAlreadyInUse, result.ResultError);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenEmailIsUnique()
    {
        //Arrange
        CreateCustomerCommand request = BuildCreateCustomerCommand();

        _customerRepositoryMock.Setup(
            e => e.IsEmailUniqueAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //Act
        Result<Guid> result = await _sut.Handle(request, default);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<Guid>(result.Value);
    }

    [Fact]
    public async Task Handle_ShouldCallAddOnRepository_WhenEmailIsUnique()
    {
        //Arrange
        CreateCustomerCommand request = BuildCreateCustomerCommand();

        _customerRepositoryMock.Setup(
            e => e.IsEmailUniqueAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //Act
        Result<Guid> result = await _sut.Handle(request, default);

        //Assert
        _customerRepositoryMock.Verify(
            x => x.Add(It.Is<Customer>(c => c.Id.Value == result.Value)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotCallSaveChanges_WhenEmailIsNotUnique()
    {
        //Arrange
        CreateCustomerCommand request = BuildCreateCustomerCommand();

        _customerRepositoryMock.Setup(
            e => e.IsEmailUniqueAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        //Act
        await _sut.Handle(request, default);

        //Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    private static CreateCustomerCommand BuildCreateCustomerCommand()
    {
        return new("Tiago",
                   "Borges",
                   "tiago@email.com",
                   new DateOnly(1990, 12, 31));
    }
}
