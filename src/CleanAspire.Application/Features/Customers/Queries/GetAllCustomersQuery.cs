// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.Customers.DTOs;

namespace CleanAspire.Application.Features.Customers.Queries;
public record GetAllCustomersQuery() : IFusionCacheRequest<List<CustomerDto>>
{
    /// <summary>
    /// Cache key for storing the results of this query.
    /// </summary>
    public string CacheKey => "getallcustomers";
    /// <summary>
    /// Tags for cache invalidation, categorizing this query under "customers".
    /// </summary>
    public IEnumerable<string>? Tags => new[] { "customers" };
}

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAllCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers
            .OrderBy(c => c.Name)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Address = c.Address,
                Created = c.Created
            })
            .ToListAsync(cancellationToken);
        return customers;
    }
}
