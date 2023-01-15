using CleanArchitectureExample.Api.Contracts;
using CleanArchitectureExample.Api.Contracts.Customers;
using CleanArchitectureExample.Application.Customers.Commands.CreateCustomer;
using CleanArchitectureExample.Application.Customers.Queries.GetCustomerById;
using CleanArchitectureExample.Application.Customers.Queries.ListCustomers;
using CleanArchitectureExample.Domain.Common.Models;
using CleanArchitectureExample.Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.Api.Controllers;

[Route("api/[controller]")]
public class CustomersController : ApiController
{
    public CustomersController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
    public async Task<IActionResult> GetCustomers(
        [FromQuery] PagedQueryRequest requestQuery,
        CancellationToken cancellationToken)
    {
        var query = new ListCustomerQuery(requestQuery.Take, requestQuery.Skip);

        Result<IList<Customer>> result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            value => Ok(
                value.Select(
                    x => new CustomerResponse(
                        x.Id.Value,
                        x.FirstName,
                        x.LastName,
                        x.Email,
                        x.DateOfBirth))),
            _ => HandleFailure(result));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);

        Result<Customer> result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            value => Ok(
                new CustomerResponse(
                    value.Id.Value,
                    value.FirstName,
                    value.LastName,
                    value.Email,
                    value.DateOfBirth)),
            _ => HandleFailure(result));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RegisterCustomer(
        [FromBody] RegisterCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth);

        Result<Guid> result = await Mediator.Send(command, cancellationToken);

        return result.Match(
            value => CreatedAtAction(
                nameof(GetCustomerById),
                new { id = value },
                value),
            _ => HandleFailure(result));
    }
}
