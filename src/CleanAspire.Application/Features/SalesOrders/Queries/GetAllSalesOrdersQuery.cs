using CleanAspire.Application.Features.SalesOrders.DTOs;

namespace CleanAspire.Application.Features.SalesOrders.Queries;
public record GetAllSalesOrdersQuery() : IFusionCacheRequest<List<SalesOrderDto>>
{
    /// <summary>
    /// Cache key for storing the results of this query.
    /// </summary>
    /// 

    public string CacheKey => "getallsalesorders";
    public IEnumerable<string>? Tags => new[] { "salesorders" };
}

public class GetAllSalesOrdersQueryHandler : IRequestHandler<GetAllSalesOrdersQuery, List<SalesOrderDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAllSalesOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<List<SalesOrderDto>> Handle(GetAllSalesOrdersQuery request, CancellationToken cancellationToken)
    {
        var salesOrders = await _context.SalesOrders
            .OrderByDescending(so => so.Created)
            .Select(so => new SalesOrderDto
            {
                Id = so.Id,
                CustomerId = so.CustomerId,
                TotalAmount = so.TotalAmount,
                CreatedDate = so.OrderDate
            })
            .ToListAsync(cancellationToken);
        return salesOrders;
    }
}
