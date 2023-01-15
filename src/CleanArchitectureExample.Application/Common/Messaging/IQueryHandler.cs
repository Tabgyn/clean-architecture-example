using CleanArchitectureExample.Domain.Common.Models;
using MediatR;

namespace CleanArchitectureExample.Application.Common.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
