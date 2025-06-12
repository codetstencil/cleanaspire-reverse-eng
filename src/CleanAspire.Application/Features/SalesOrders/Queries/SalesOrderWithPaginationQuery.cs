// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CleanAspire.Application.Features.SalesOrders.DTOs;

namespace CleanAspire.Application.Features.SalesOrders.Queries;
public record SalesOrderWithPaginationQuery(string Keywords, int PageNumber = 0, int PageSize = 15, string OrderBy = "Id", string SortDirection = "Descending") : IFusionCacheRequest<PaginatedResult<SalesOrderDto>>
{
    public IEnumerable<string>? Tags => new[] { "salesorders" };
    public string CacheKey => $"salesorderswithpagination_{Keywords}_{PageNumber}_{PageSize}_{OrderBy}_{SortDirection}";
}

public class SalesOrderWithPaginationQueryHandler : IRequestHandler<SalesOrderWithPaginationQuery, PaginatedResult<SalesOrderDto>>
{
    private readonly IApplicationDbContext _context;
    public SalesOrderWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<PaginatedResult<SalesOrderDto>> Handle(SalesOrderWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.SalesOrders.OrderBy(request.OrderBy, request.SortDirection)
                    .ProjectToPaginatedDataAsync(
                        condition: x => x.Customer.Name.Contains(request.Keywords) || x.Status.Contains(request.Keywords),
                        pageNumber: request.PageNumber,
                        pageSize: request.PageSize,
                        mapperFunc: t => new SalesOrderDto
                        {
                            Id = t.Id,
                            CustomerName = t.Customer?.Name,
                            TotalAmount = t.TotalAmount,
                            CreatedDate = t.OrderDate
                        },
                    cancellationToken: cancellationToken);
        return data;
    }
}
