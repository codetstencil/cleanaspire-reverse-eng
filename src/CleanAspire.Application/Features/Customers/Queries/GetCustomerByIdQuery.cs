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
public record GetCustomerByIdQuery(string Id) : IFusionCacheRequest<CustomerDto?>
{
    /// <summary>
    /// Cache key for storing the result of this query, specific to the customer ID.
    /// </summary>
    public string CacheKey => $"customer_{Id}";
    /// <summary>
    /// Tags for cache invalidation, categorizing this query under "customers".
    /// </summary>
    public IEnumerable<string>? Tags => new[] { "customers" };
}

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly IApplicationDbContext _context;
    public GetCustomerByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Product with Id '{request.Id}' was not found.");
        }
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            Address = customer.Address,
            Created = customer.Created
        };
    }
}
