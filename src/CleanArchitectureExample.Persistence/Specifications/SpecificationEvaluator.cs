using CleanArchitectureExample.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureExample.Persistence.Specifications;

internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity, TId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId> specification)
        where TEntity : Entity<TId>
        where TId : notnull
    {
        IQueryable<TEntity> queryable = inputQueryable;

        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification.IncludeExpressions.Aggregate(
            queryable,
            (current, includeExpressions) =>
            current.Include(includeExpressions));

        queryable = specification.IncludeStrings.Aggregate(
            queryable,
            (current, include) =>
            current.Include(include));

        if (specification.OrderByExpression is not null)
        {
            queryable.OrderBy(specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (specification.GroupBy is not null)
        {
            queryable = queryable
                .GroupBy(specification.GroupBy)
                .SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            queryable = queryable
                .Skip(specification.Skip)
                .Take(specification.Take);
        }

        if (specification.AsNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (specification.IsSplitQuery)
        {
            queryable = queryable.AsSplitQuery();
        }

        return queryable;
    }
}
