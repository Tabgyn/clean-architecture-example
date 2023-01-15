using CleanArchitectureExample.Application.Customers.Queries.GetCustomerById;
using CleanArchitectureExample.Domain.Common.Errors;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;
using Moq;
using Xunit;

namespace CleanArchitectureExample.UnitTest.Application.Customers;

public class GetCustomerByIdQueryHandlerTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly GetCustomerByIdQueryHandler _sut;

    public GetCustomerByIdQueryHandlerTest()
    {
        _customerRepositoryMock = new();
        _sut = new GetCustomerByIdQueryHandler(_customerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenCustomerIsNotFound()
    {
        //Arrange
        GetCustomerByIdQuery request = BuildGetCustomerByIdQuery();

        _customerRepositoryMock.Setup(
            e => e.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        //Act
        Result<Customer> result = await _sut.Handle(request, default);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            Errors.General.NotFound(request.CustomerId),
            result.ResultError);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenCustomerIsFound()
    {
        //Arrange
        GetCustomerByIdQuery request = BuildGetCustomerByIdQuery();
        Customer customer = BuildCustomer();

        _customerRepositoryMock.Setup(
            e => e.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        //Act
        Result<Customer> result = await _sut.Handle(request, default);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<Customer>(result.Value);
    }

    [Fact]
    public async Task Handle_ShouldCallGetByIdOnRepositoryOnce()
    {
        //Arrange
        GetCustomerByIdQuery request = BuildGetCustomerByIdQuery();
        Customer customer = BuildCustomer();

        _customerRepositoryMock.Setup(
            e => e.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        //Act
        await _sut.Handle(request, default);

        //Assert
        _customerRepositoryMock.Verify(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static GetCustomerByIdQuery BuildGetCustomerByIdQuery()
    {
        return new(It.IsAny<Guid>());
    }

    private static Customer BuildCustomer()
    {
        return Customer.Create(
            "Tiago",
            "Borges",
            "tiago@email.com",
            new DateOnly(1990, 12, 31));
    }
}
