using CleanAspire.Application.Features.Customers.DTOs;

namespace CleanAspire.Application.Features.Customers.Queries;
public record CustomersWithPaginationQuery(string Keywords, int PageNumber = 0, int PageSize = 15, string OrderBy = "Id", string SortDirection = "Descending") : IFusionCacheRequest<PaginatedResult<CustomerDto>>
{
    public IEnumerable<string>? Tags => new[] { "customers" };
    public string CacheKey => $"customerswithpagination_{Keywords}_{PageNumber}_{PageSize}_{OrderBy}_{SortDirection}";
}

public class CustomersWithPaginationQueryHandler : IRequestHandler<CustomersWithPaginationQuery, PaginatedResult<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    public CustomersWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<PaginatedResult<CustomerDto>> Handle(CustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Customers.OrderBy(request.OrderBy, request.SortDirection)
                    .ProjectToPaginatedDataAsync(
                        condition: x => x.Name.Contains(request.Keywords) || x.Email.Contains(request.Keywords) || x.PhoneNumber.Contains(request.Keywords),
                        pageNumber: request.PageNumber,
                        pageSize: request.PageSize,
                        mapperFunc: t => new CustomerDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Email = t.Email,
                            PhoneNumber = t.PhoneNumber,
                            Created = t.Created,
                            LastModified = t.LastModified
                        },
                    cancellationToken: cancellationToken);
        return data;
    }
}
