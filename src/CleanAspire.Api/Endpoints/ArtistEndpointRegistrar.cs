using CleanAspire.Application.Common.Models;
using CleanAspire.Application.Features.Artists.Commands;
using CleanAspire.Application.Features.Artists.DTOs;
using CleanAspire.Application.Features.Artists.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanAspire.Api.Endpoints;

public class ArtistEndpointRegistrar(ILogger<ArtistEndpointRegistrar> logger) : IEndpointRegistrar
{
    public void RegisterRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/artists").WithTags("artists").RequireAuthorization();
        // Get artists with pagination
        group.MapPost("/pagination", ([FromServices] IMediator mediator, [FromBody] ArtistsWithPaginationQuery query) => mediator.Send(query))
            .Produces<PaginatedResult<ArtistDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get artists with pagination")
            .WithDescription("Returns a paginated list of artists based on search keywords, page size, and sorting options.");

        // Get all artists
        group.MapGet("/", async ([FromServices] IMediator mediator) => await mediator.Send(new GetAllArtistsQuery()))
            .Produces<IEnumerable<ArtistDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all artists")
            .WithDescription("Returns a list of all artists.");

        // Get artist by ID
        group.MapGet("/{id}", (IMediator mediator, [FromRoute] int id) => mediator.Send(new GetArtistByIdQuery(id)))
            .Produces<ArtistDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get artist by ID")
            .WithDescription("Returns the details of a specific artist by their unique ID.");

        // Update artist
        group.MapPut("/", ([FromServices] IMediator mediator, [FromBody] UpdateArtistCommand command) => mediator.Send(command))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update artist")
            .WithDescription("Updates the details of an existing artist.");

        //create artist
        group.MapPost("/", ([FromServices] IMediator mediator, [FromBody] CreateArtistCommand command) => mediator.Send(command))
            .Produces<ArtistDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create artist")
            .WithDescription("Creates a new artist record.");
    }
}
