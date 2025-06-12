// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.SalesOrders.DTOs;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.SalesOrders.Commands;
public record CreateSalesOrderCommand(
    int Quantity,
    decimal Price
) : IFusionCacheRefreshRequest<SalesOrderDto>, IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "salesorders" };
}

public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, SalesOrderDto>
{
    private readonly IApplicationDbContext _context;
    public CreateSalesOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<SalesOrderDto> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = new SalesOrder
        {
            Quantity = request.Quantity,
            TotalAmount = request.Price
        };
        _context.SalesOrders.Add(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);
        return new SalesOrderDto
        {
            Id = salesOrder.Id,
            Quantity = salesOrder.Quantity,
            TotalAmount = salesOrder.TotalAmount,
            CreatedDate = salesOrder.OrderDate
        };
    }
}
