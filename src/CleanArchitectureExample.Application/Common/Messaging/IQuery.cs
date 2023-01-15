using CleanArchitectureExample.Domain.Common.Models;
using MediatR;

namespace CleanArchitectureExample.Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
