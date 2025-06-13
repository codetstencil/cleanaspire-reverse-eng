// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Artists.Commands;
public record DeleteArtistCommand(
    int ArtistId
) : IFusionCacheRefreshRequest<Unit>,
    IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "artists" };
}

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FindAsync(new object[] { request.ArtistId }, cancellationToken);
        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID '{request.ArtistId}' was not found.");
        }
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
