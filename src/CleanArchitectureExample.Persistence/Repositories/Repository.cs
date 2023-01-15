using CleanArchitectureExample.Domain.Common.Interfaces.Persistence;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Persistence.Specifications;

namespace CleanArchitectureExample.Persistence.Repositories;

internal abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    public readonly ApplicationDbContext DbContext;

    public Repository(ApplicationDbContext dbContext) => DbContext = dbContext;

    public IQueryable<TEntity> ApplySpecification(Specification<TEntity, TId> specification)
    {
        return SpecificationEvaluator.GetQuery(
            DbContext.Set<TEntity>(),
            specification);
    }
}
