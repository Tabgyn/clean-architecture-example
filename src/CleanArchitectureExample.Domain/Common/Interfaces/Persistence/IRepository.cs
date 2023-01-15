namespace CleanArchitectureExample.Domain.Common.Interfaces.Persistence;

public interface IRepository<TEntity, TId>
    where TEntity : class
    where TId : notnull
{
}
