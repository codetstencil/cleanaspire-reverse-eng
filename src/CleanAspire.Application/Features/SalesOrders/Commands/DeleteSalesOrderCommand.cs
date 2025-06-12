// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.SalesOrders.EventHandlers;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.SalesOrders.Commands;
public record DeleteSalesOrderCommand(string Id) : IFusionCacheRefreshRequest<Unit>, IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "salesorders" };
}

public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteSalesOrderCommandHandler(ILogger<DeleteSalesOrderCommandHandler> logger, IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrders = _context.SalesOrders.Where(c => request.Id.Contains(c.Id));
        foreach (var salesOrder in salesOrders)
        {
            salesOrder.AddDomainEvent(new SalesOrderDeletedEvent(salesOrder));
            _context.SalesOrders.Remove(salesOrder);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
