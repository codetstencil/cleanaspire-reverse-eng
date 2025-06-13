// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.Artists.DTOs;

namespace CleanAspire.Application.Features.Artists.Queries;
public record GetArtistByIdQuery(int Id) : IFusionCacheRequest<ArtistDto?>
{
    /// <summary>
    /// Cache key for storing the result of this query, specific to the artist ID.
    /// </summary>
    public string CacheKey => $"artist_{Id}";

    /// <summary>
    /// Tags for cache invalidation, categorizing this query under "artists".
    /// </summary>
    public IEnumerable<string>? Tags => new[] { "artists" };
}

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistDto?>
{
    private readonly IApplicationDbContext _context;
    public GetArtistByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<ArtistDto?> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists
            .FirstOrDefaultAsync(a => a.ArtistId == request.Id, cancellationToken);
        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with Id '{request.Id}' was not found.");
        }
        return new ArtistDto
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name
        };
    }
}
