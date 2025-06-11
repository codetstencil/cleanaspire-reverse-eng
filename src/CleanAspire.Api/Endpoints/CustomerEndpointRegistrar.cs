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
    }
}
