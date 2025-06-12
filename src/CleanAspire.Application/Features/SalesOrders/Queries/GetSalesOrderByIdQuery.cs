// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.SalesOrders.DTOs;

namespace CleanAspire.Application.Features.SalesOrders.Queries;
public record GetSalesOrderByIdQuery(string Id) : IFusionCacheRequest<SalesOrderDto?>
{
    /// <summary>
    /// Cache key for storing the result of this query, specific to the sales order ID.
    /// </summary>
    public string CacheKey => $"salesorder_{Id}";
    /// <summary>
    /// Tags for cache invalidation, categorizing this query under "salesorders".
    /// </summary>
    public IEnumerable<string>? Tags => new[] { "salesorders" };
}

public class GetSalesOrderByIdQueryHandler : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDto?>
{
    private readonly IApplicationDbContext _context;
    public GetSalesOrderByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<SalesOrderDto?> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders
            .Where(so => so.Id == request.Id)
            .Select(so => new SalesOrderDto
            {
                Id = so.Id,
                CustomerId = so.CustomerId,
                CustomerName = so.Customer!.Name,
                CustomerEmail = so.Customer.Email,
                TotalAmount = so.TotalAmount,
                Status = so.Status,
                CreatedDate = so.OrderDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (salesOrder is null)
            throw new KeyNotFoundException($"Sales order with Id '{request.Id}' was not found.");

        return salesOrder;



    }
}
