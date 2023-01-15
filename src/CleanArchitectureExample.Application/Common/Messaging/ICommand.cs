using CleanArchitectureExample.Domain.Common.Models;
using MediatR;

namespace CleanArchitectureExample.Application.Common.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
