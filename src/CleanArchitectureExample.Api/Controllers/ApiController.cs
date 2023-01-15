using CleanArchitectureExample.Domain.Common.Enums;
using CleanArchitectureExample.Domain.Common.Interfaces;
using CleanArchitectureExample.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Mediator;

    protected ApiController(ISender sender) => Mediator = sender;

    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        if (result is IValidationResult validationResult)
        {
            return BadRequest(
                CreateProblemDetails(
                    "Validation error",
                    StatusCodes.Status400BadRequest,
                    result.ResultError,
                    validationResult.Errors));
        }

        (string title, int statusCode) = result.ResultError.Type switch
        {
            ErrorType.Conflict => ("A conflict has occurred", StatusCodes.Status409Conflict),
            ErrorType.Failure => ("An error occurred", StatusCodes.Status400BadRequest),
            ErrorType.NotFound => ("Not found", StatusCodes.Status404NotFound),
            ErrorType.Validation => ("Validation error", StatusCodes.Status400BadRequest),
            _ or ErrorType.Unexpected => ("An error occurred", StatusCodes.Status500InternalServerError)
        };

        return Problem(
            title: title,
            detail: result.ResultError.Message,
            statusCode: statusCode,
            type: result.ResultError.Code);
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
