using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Persistence.Specifications;

[ExcludeFromCodeCoverage]
public abstract class Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    protected Specification(Expression<Func<TEntity, bool>>? criteria) =>
        Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
    public List<string> IncludeStrings { get; } = new List<string>();
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }
    public Expression<Func<TEntity, object>>? GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; } = false;
    public bool AsNoTracking { get; protected set; } = false;
    public bool IsSplitQuery { get; protected set; } = false;

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);

    protected virtual void AddInclude(string includeString) =>
        IncludeStrings.Add(includeString);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;

    protected virtual void AddGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }

    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
