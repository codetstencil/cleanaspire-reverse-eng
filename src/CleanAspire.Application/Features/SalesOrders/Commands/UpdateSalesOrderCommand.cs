// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.SalesOrders.Commands;
public record UpdateSalesOrderCommand(
    string Id,
    string CustomerId,
    DateTime OrderDate,
    decimal TotalAmount
) : IFusionCacheRefreshRequest<Unit>,
    IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "salesorders" };
}

public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateSalesOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken);
        if (salesOrder == null)
        {
            throw new KeyNotFoundException($"Sales order with ID '{request.Id}' was not found.");
        }
        salesOrder.CustomerId = request.CustomerId;
        salesOrder.OrderDate = request.OrderDate;
        salesOrder.TotalAmount = request.TotalAmount;
        _context.SalesOrders.Update(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
