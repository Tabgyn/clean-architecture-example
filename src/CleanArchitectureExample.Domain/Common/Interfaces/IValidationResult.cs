using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Common.Interfaces;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "A validation problem occurred.");

    Error[] Errors { get; }
}
