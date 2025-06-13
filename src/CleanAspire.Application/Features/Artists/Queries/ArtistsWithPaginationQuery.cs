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
public record ArtistsWithPaginationQuery(string Keywords, int PageNumber = 0, int PageSize = 15, string OrderBy = "Id", string SortDirection = "Descending") : IFusionCacheRequest<PaginatedResult<ArtistDto>>
{
    public IEnumerable<string>? Tags => new[] { "artists" };
    public string CacheKey => $"artistswithpagination_{Keywords}_{PageNumber}_{PageSize}_{OrderBy}_{SortDirection}";
}

public class ArtistsWithPaginationQueryHandler : IRequestHandler<ArtistsWithPaginationQuery, PaginatedResult<ArtistDto>>
{
    private readonly IApplicationDbContext _context;
    public ArtistsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PaginatedResult<ArtistDto>> Handle(ArtistsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Artists.OrderBy(request.OrderBy, request.SortDirection)
                    .ProjectToPaginatedDataAsync(
                        condition: x => x.Name.Contains(request.Keywords),
                        pageNumber: request.PageNumber,
                        pageSize: request.PageSize,
                        mapperFunc: t => new ArtistDto
                        {
                            ArtistId = t.ArtistId,
                            Name = t.Name
                        },
                    cancellationToken: cancellationToken);
        return data;
    }
}
