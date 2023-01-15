using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Orders;

public class Order : Entity<Guid>
{
    public Order(Guid id, DateTime date, decimal total)
        : base(id)
    {
        Date = date;
        Total = total;
    }

    public DateTime Date { get; }
    public decimal Total { get; }
}
