namespace CleanArchitectureExample.Domain.Customers.Interfaces.Persistence;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<IList<Customer>> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default);
}
