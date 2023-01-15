using CleanArchitectureExample.Domain.Common.Interfaces;

namespace CleanArchitectureExample.Domain.Customers.Events;

public sealed class CustomerCreated : IDomainEvent
{
    public CustomerCreated(string customerId, string name, string email)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
        OccurredOn = DateTime.UtcNow;
    }

    public string CustomerId { get; }
    public string Name { get; }
    public string Email { get; }
    public DateTime OccurredOn { get; }
}
