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
public record GetAllArtistsQuery() : IFusionCacheRequest<List<ArtistDto>>
{
    /// <summary>
    /// Cache key for storing the results of this query.
    /// </summary>
    public string CacheKey => "getallartists";

    /// <summary>
    /// Tags for cache invalidation, categorizing this query under "artists".
    /// </summary>
    public IEnumerable<string>? Tags => new[] { "artists" };
}

public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, List<ArtistDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAllArtistsQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<List<ArtistDto>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await _context.Artists
            .OrderBy(a => a.Name)
            .Select(a => new ArtistDto
            {
                ArtistId = a.ArtistId,
                Name = a.Name
            })
            .ToListAsync(cancellationToken);
        return artists;
    }
}
