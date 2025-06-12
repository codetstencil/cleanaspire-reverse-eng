// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CleanAspire.Application.Common.Models;
using CleanAspire.Application.Features.Customers.Commands;
using CleanAspire.Application.Features.Customers.DTOs;
using CleanAspire.Application.Features.Customers.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanAspire.Api.Endpoints;

public class CustomerEndpointRegistrar(ILogger<CustomerEndpointRegistrar> logger) : IEndpointRegistrar
{
    public void RegisterRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/customers").WithTags("customers").RequireAuthorization();

        // Receive customer
        group.MapPost("/receive", ([FromServices] IMediator mediator, [FromBody] CustomerReceivingCommand command) => mediator.Send(command))
            .Produces<Unit>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Receive customer")
            .WithDescription("Receives a new customer record.");

        // Get customers with pagination
        group.MapPost("/pagination", ([FromServices] IMediator mediator, [FromBody] CustomersWithPaginationQuery query) => mediator.Send(query))
            .Produces<PaginatedResult<CustomerDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get customers with pagination")
            .WithDescription("Returns a paginated list of customers based on search keywords, page size, and sorting options.");


        // Get all customers
        group.MapGet("/",async ([FromServices]IMediator mediator) => await mediator.Send(new GetAllCustomersQuery()))
            .Produces<IEnumerable<CustomerDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all customers")
            .WithDescription("Returns a list of all customers.");

        // Get customer by ID
        group.MapGet("/{id}", (IMediator mediator, [FromRoute] string id) => mediator.Send(new GetCustomerByIdQuery(id)))
            .Produces<CustomerDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get customer by ID")
            .WithDescription("Returns the details of a specific customer by their unique ID.");

        // Update customer
        group.MapPut("/", ([FromServices] IMediator mediator, [FromBody] UpdateCustomerCommand command) => mediator.Send(command))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update customer")
            .WithDescription("Updates an existing customer's details.");

        // Delete customer
        group.MapDelete("/", ([FromServices] IMediator mediator, [FromBody] DeleteCustomerCommand command) => mediator.Send(command))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete customer")
            .WithDescription("Deletes a customer by their unique ID.");

    }
}
