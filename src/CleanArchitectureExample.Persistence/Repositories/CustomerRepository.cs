using CleanArchitectureExample.Domain.Customers;
using CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;
using CleanArchitectureExample.Domain.Customers.ValueObjects;
using CleanArchitectureExample.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureExample.Persistence.Repositories;

internal sealed class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public void Add(Customer customer) => DbContext.Set<Customer>().Add(customer);

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await ApplySpecification(new CustomerByIdSpecification(id))
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default) =>
        !await ApplySpecification(new CustomerByEmailSpecification(email))
        .AnyAsync(cancellationToken);

    public async Task<IList<Customer>> GetAllAsync(
        int take,
        int skip,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(new CustomerPagedSpecification(take, skip))
        .ToListAsync(cancellationToken);
}
