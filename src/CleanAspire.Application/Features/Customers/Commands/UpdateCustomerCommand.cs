// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Customers.Commands;
public record UpdateCustomerCommand(
    string Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address
) : IFusionCacheRefreshRequest<Unit>,
    IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "customers" };
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID '{request.Id}' was not found.");
        }
        customer.Name = request.Name;
        customer.PhoneNumber = request.PhoneNumber;
        customer.Email = request.Email;
        customer.Address = request.Address;
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
