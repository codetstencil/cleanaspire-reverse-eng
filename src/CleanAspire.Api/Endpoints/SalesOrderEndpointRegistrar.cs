using CleanAspire.Application.Common.Models;
using CleanAspire.Application.Features.SalesOrders.Commands;
using CleanAspire.Application.Features.SalesOrders.DTOs;
using CleanAspire.Application.Features.SalesOrders.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanAspire.Api.Endpoints;

public class SalesOrderEndpointRegistrar(ILogger<SalesOrderEndpointRegistrar> logger) : IEndpointRegistrar
{
    public void RegisterRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/salesorders").WithTags("salesorders").RequireAuthorization();
        // Create Sales Order
        group.MapPost("/", ([FromServices] IMediator mediator, [FromBody] CreateSalesOrderCommand command) => mediator.Send(command))
            .Produces<Unit>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create Sales Order")
            .WithDescription("Creates a new sales order record.");
        // Get Sales Orders with Pagination
        group.MapPost("/pagination", ([FromServices] IMediator mediator, [FromBody] SalesOrderWithPaginationQuery query) => mediator.Send(query))
            .Produces<PaginatedResult<SalesOrderDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Sales Orders with Pagination")
            .WithDescription("Returns a paginated list of sales orders based on search keywords, page size, and sorting options.");
        // Get All Sales Orders
        group.MapGet("/", async ([FromServices] IMediator mediator) => await mediator.Send(new GetAllSalesOrdersQuery()))
            .Produces<IEnumerable<SalesOrderDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get All Sales Orders")
            .WithDescription("Returns a list of all sales orders.");
        // Get Sales Order by ID
        group.MapGet("/{id}", (IMediator mediator, [FromRoute] string id) => mediator.Send(new GetSalesOrderByIdQuery(id)))
            .Produces<SalesOrderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get Sales Order by ID")
            .WithDescription("Returns the details of a specific sales order by its unique ID.");
        // Update Sales Order
        group.MapPut("/", ([FromServices] IMediator mediator, [FromBody] UpdateSalesOrderCommand command) => mediator.Send(command))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update Sales Order")
            .WithDescription("Updates an existing sales order details.");
    }
}
