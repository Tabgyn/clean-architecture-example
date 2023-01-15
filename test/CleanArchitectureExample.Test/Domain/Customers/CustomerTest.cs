using CleanArchitectureExample.Domain.Common.Exceptions;
using CleanArchitectureExample.Domain.Customers;
using Xunit;

namespace CleanArchitectureExample.UnitTest.Domain.Customers;

public class CustomerTest
{
    [Fact]
    public void Customer_ShouldCreateInstance()
    {
        //Arrange
        //Arrange
        string firstName = "Tiago";
        string lastName = "Borges";
        string email = "tiago@email.com";
        DateOnly dateOfBirth = new(1990, 12, 31);

        //Act
        var customer = Customer.Create(
            firstName,
            lastName,
            email,
            dateOfBirth);

        //Assert
        Assert.NotNull(customer.Id);
        Assert.Equal(firstName, customer.FirstName);
        Assert.Equal(lastName, customer.LastName);
        Assert.Equal(email, customer.Email);
        Assert.Equal(dateOfBirth, customer.DateOfBirth);
    }

    [Fact]
    public void Customer_ShouldThrowInvalidEmailException_WhenEmailNull()
    {
        //Arrange
        string firstName = "Tiago";
        string lastName = "Borges";
        string email = null!;
        DateOnly dateOfBirth = new(1990, 12, 31);

        //Act
        void Action() => Customer.Create(
            firstName,
            lastName,
            email,
            dateOfBirth);

        //Assert
        Assert.Throws<InvalidEmailException>(Action);
    }

    [Fact]
    public void Customer_ShouldThrowInvalidEmailException_WhenEmailInvalidLength()
    {
        //Arrange
        string firstName = "Tiago";
        string lastName = "Borges";
        string email = "mail";
        DateOnly dateOfBirth = new(1990, 12, 31);

        //Act
        void Action() => Customer.Create(
            firstName,
            lastName,
            email,
            dateOfBirth);

        //Assert
        Assert.Throws<InvalidEmailException>(Action);
    }

    [Fact]
    public void Customer_ShouldThrowInvalidEmailException_WhenEmailHasInvalidPattern()
    {
        //Arrange
        string firstName = "Tiago";
        string lastName = "Borges";
        string email = "tiago#email.com";
        DateOnly dateOfBirth = new(1990, 12, 31);

        //Act
        void Action() => Customer.Create(
            firstName,
            lastName,
            email,
            dateOfBirth);

        //Assert
        Assert.Throws<InvalidEmailException>(Action);
    }
}
